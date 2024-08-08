namespace K8sUtils.Models.GetPodsResponse;

public record PodMetadata(
    DateTime CreationTimestamp,
    string GenerateName,
    Dictionary<string, string> Labels,
    string Name,
    string Namespace,
    List<OwnerReference> OwnerReferences,
    string ResourceVersion,
    string Uid
);