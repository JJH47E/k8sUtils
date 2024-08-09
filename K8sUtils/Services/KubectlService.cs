using K8sUtils.Exceptions;
using K8sUtils.Models.Context;
using K8sUtils.Models.GetPodsResponse;
using K8sUtils.ProcessHosts;

namespace K8sUtils.Services;

public class KubectlService(IKubectlHost kubectlHost) : IKubectlService
{
    public async Task<IEnumerable<PodItem>> GetPodsAsync(string @namespace)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
        {
            throw new ArgumentException("Namespace cannot be null or empty", nameof(@namespace));
        }
        
        var response = await kubectlHost.ListPods(@namespace);
        var pods = response.Items;
        
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

    public async Task<IEnumerable<Context>> GetContextsAsync()
    {
        var currentContextTask = kubectlHost.GetCurrentContext();
        var allContextsTask = kubectlHost.GetAllContexts();
        
        await Task.WhenAll(currentContextTask, allContextsTask);
        
        var currentContext = currentContextTask.Result;
        var allContexts = allContextsTask.Result;

        return allContexts.Select(ctx => new Context()
        {
            Name = ctx,
            IsCurrent = string.Equals(currentContext, ctx, StringComparison.InvariantCultureIgnoreCase)
        });
    }

    public Task<string> GetCurrentContextAsync()
    {
        return kubectlHost.GetCurrentContext();
    }
}