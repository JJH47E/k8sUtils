namespace K8sUtils.Models.GetPodsResponse;

public record Condition(
    DateTime? LastProbeTime,
    DateTime LastTransitionTime,
    string Status,
    string Type
);