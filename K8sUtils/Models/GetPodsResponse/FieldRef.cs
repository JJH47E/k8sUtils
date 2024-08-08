namespace K8sUtils.Models.GetPodsResponse;

public record FieldRef(
    string ApiVersion,
    string FieldPath
);