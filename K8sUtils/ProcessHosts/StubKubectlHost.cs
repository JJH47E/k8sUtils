namespace K8sUtils.ProcessHosts;

public class StubKubectlHost : IKubectlHost
{
    public Task<IEnumerable<string>> ListPods(string @namespace)
    {
        var pods = new List<string> { "pod1", "pod2", "pod3" };
        return Task.FromResult(pods.AsEnumerable());
    }

    public Task<IEnumerable<string>> GetLogs(string podName, string @namespace)
    {
        var logs = Enumerable.Range(1, 10).Select(i => $"{podName} log {i}").ToList();

        return Task.FromResult(logs.AsEnumerable());
    }
}