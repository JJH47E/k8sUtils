using K8sUtils.Exceptions;
using K8sUtils.ProcessHosts;

namespace K8sUtils.Services;

public class KubectlService(IKubectlHost kubectlHost) : IKubectlService
{
    public async Task<IEnumerable<string>> GetPodsAsync(string @namespace)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
        {
            throw new ArgumentException("Namespace cannot be null or empty", nameof(@namespace));
        }
        
        var pods = (await kubectlHost.ListPods(@namespace)).ToList();
        
        if (pods.Count == 0)
        {
            throw new KubectlRuntimeException(
                $"Unable to find any pods in the namespace: {@namespace}. Is it correct?");
        }

        return pods;
    }

    public Task<IEnumerable<string>> GetLogsAsync(string podName, string @namespace)
    {
        if (string.IsNullOrWhiteSpace(podName))
        {
            throw new ArgumentException("Pod name cannot be null or empty", nameof(podName));
        }

        if (string.IsNullOrWhiteSpace(@namespace))
        {
            throw new ArgumentException("Namespace cannot be null or empty", nameof(@namespace));
        }
        
        return kubectlHost.GetLogs(podName, @namespace);
    }
}