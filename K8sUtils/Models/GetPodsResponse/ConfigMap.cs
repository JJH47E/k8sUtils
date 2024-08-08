namespace K8sUtils.Models.GetPodsResponse;

public record ConfigMap(
    List<ConfigMapItem> Items,
    string Name
);