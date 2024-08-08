namespace K8sUtils.Models.GetPodsResponse;

public enum PodStatus
{
    Unknown,
    Pending,
    Running,
    Succeeded,
    Failed
}