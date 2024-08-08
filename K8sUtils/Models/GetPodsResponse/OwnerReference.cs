namespace K8sUtils.Models.GetPodsResponse;

public record OwnerReference(
    string ApiVersion,
    bool BlockOwnerDeletion,
    bool Controller,
    string Kind,
    string Name,
    string Uid
);