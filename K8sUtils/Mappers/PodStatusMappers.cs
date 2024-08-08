using K8sUtils.Models.GetPodsResponse;

namespace K8sUtils.Mappers;

public static class PodStatusMappers
{
    public static string StatusToIcon(PodStatus podStatus)
    {
        return podStatus switch
        {
            PodStatus.Pending => "\u27f3",
            PodStatus.Running => "\u2713",
            PodStatus.Succeeded => "Succeeded",
            PodStatus.Failed => "\u26cc",
            _ => "Unknown",
        };
    }
}