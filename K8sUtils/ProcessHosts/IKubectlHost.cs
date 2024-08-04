using K8sUtils.Models.GetPodsResponse;

namespace K8sUtils.ProcessHosts;

public interface IKubectlHost
{
    Task<Root> ListPods(string @namespace);
    Task<IEnumerable<string>> GetLogs(string podName, string @namespace);
}