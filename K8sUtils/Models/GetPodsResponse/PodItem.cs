namespace K8sUtils.Models.GetPodsResponse;

public record PodItem(
    string ApiVersion,
    string Kind,
    PodMetadata Metadata,
    Spec Spec,
    Status Status
)
{
    public sealed override string ToString() => Metadata.Name;
    public string? GetServiceName() => Metadata.Labels.GetValueOrDefault("app");
    public string GetNamespace() => Metadata.Namespace;
    public string? GetCommitHash() => Metadata.Labels.GetValueOrDefault("pod-template-hash");
    public PodStatus? GetStatus() => Status.Phase;
};