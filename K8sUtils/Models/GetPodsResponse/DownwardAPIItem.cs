namespace K8sUtils.Models.GetPodsResponse;

public record DownwardAPIItem(
    FieldRef FieldRef,
    string Path
);