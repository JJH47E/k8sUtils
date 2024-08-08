namespace K8sUtils.Models.GetPodsResponse;

public record Toleration(
    string Effect,
    string Key,
    string Operator,
    int TolerationSeconds
);