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
    public string Namespace => Metadata.Namespace;
    public PodStatus? PodStatus => Status.Phase;
    public string Kind => Kind;
    public DateTimeOffset Created => Metadata.CreationTimestamp;
    public DateTimeOffset Started => Status.StartTime;
    public Dictionary<string, string> Labels => Metadata.Labels;
};