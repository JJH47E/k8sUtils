namespace K8sUtils.Models.GetPodsResponse;

public record Projected(
    int DefaultMode,
    List<Source> Sources
);