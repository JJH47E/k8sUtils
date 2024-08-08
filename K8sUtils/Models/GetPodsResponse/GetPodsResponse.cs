namespace K8sUtils.Models.GetPodsResponse;

public record GetPodsResponse(
    string ApiVersion,
    List<PodItem> Items,
    string Kind,
    PodMetadata Metadata
);