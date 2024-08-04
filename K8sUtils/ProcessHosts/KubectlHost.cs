using System.Diagnostics;
using System.Runtime.InteropServices;
using K8sUtils.Exceptions;
using K8sUtils.Models.GetPodsResponse;
using Newtonsoft.Json;

namespace K8sUtils.ProcessHosts;

public class KubectlHost : IKubectlHost
{
    private const string KubectlCommand = "kubectl";

    public KubectlHost()
    {
        if (!IsKubectlInstalled())
        {
            throw new Exception("kubectl is not installed or not found in the system path.");
        }
    }

    private bool IsKubectlInstalled()
    {
        var command = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "where" : "which";
        var processInfo = new ProcessStartInfo(command, KubectlCommand)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = processInfo;
        process.Start();
        process.WaitForExit();

        var output = process.StandardOutput.ReadToEnd();
        return !string.IsNullOrEmpty(output) && File.Exists(output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim());
    }

    public async Task<Root> ListPods(string @namespace)
    {
        var processInfo = new ProcessStartInfo(KubectlCommand, $"get pods -n {@namespace} -o json")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = processInfo;
        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new KubectlRuntimeException("Kubectl exited with a non 0 error code. This may be a bug.");
        }

        var parsedResult = JsonConvert.DeserializeObject<Root>(output);

        if (parsedResult is null)
        {
            throw new KubectlRuntimeException("Kubectl output did not match expected data structure.");
        }

        return parsedResult;
    }
    
    public async Task<IEnumerable<string>> GetLogs(string podName, string @namespace)
    {
        var processInfo = new ProcessStartInfo(KubectlCommand, $"logs {podName} -n {@namespace}")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = processInfo;
        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new Exception($"kubectl command failed: {error}");
        }

        return output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }
}