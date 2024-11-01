namespace K8sUtils.Mappers;

public static class CommandMappers
{
    public static string GetPortForwardCommand(string @namespace, string podName) => $"kubectl port-forward -n {@namespace} {podName} :80";

    public static string GetShellCommand(string @namespace, string podName) =>
        $"kubectl exec -it {podName} -n {@namespace} -- bash";

    public static string GetStreamLogsCommand(string @namespace, string podName) =>
        $"kubectl logs -f {podName} -n {@namespace}";
}
