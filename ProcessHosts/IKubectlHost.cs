namespace K8sUtils.ProcessHosts;

public interface IKubectlHost
{
    string[] ListPods(string @namespace);
}