namespace K8sUtils.Models.GetPodsResponse;

public record VolumeMount(
    string MountPath,
    string Name,
    bool ReadOnly
);