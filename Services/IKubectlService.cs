namespace K8sUtils.Services;

public interface IKubectlService
{
    Task<IEnumerable<string>> GetPodsAsync(string @namespace);
    Task<IEnumerable<string>> GetLogsAsync(string podName, string @namespace);
}