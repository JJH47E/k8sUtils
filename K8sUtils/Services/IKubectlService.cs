using K8sUtils.Models.GetPodsResponse;

namespace K8sUtils.Services;

public interface IKubectlService
{
    Task<IEnumerable<Item>> GetPodsAsync(string @namespace);
    Task<IEnumerable<string>> GetLogsAsync(string podName, string @namespace);
}