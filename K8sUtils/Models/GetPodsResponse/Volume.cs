namespace K8sUtils.Models.GetPodsResponse;

public record Volume(
    string Name,
    Projected Projected
);