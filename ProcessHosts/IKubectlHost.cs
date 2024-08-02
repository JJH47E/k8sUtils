namespace K8sUtils.ProcessHosts;

public interface IKubectlHost
{
    string[] ListPods(string @namespace);
    string[] GetLogs(string podName, string @namespace);
}