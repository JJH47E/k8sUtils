namespace K8sUtils.ProcessHosts;

public interface IKubectlHost
{
    Task<IEnumerable<string>> ListPods(string @namespace);
    Task<IEnumerable<string>> GetLogs(string podName, string @namespace);
}