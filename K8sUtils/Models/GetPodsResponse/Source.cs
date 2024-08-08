namespace K8sUtils.Models.GetPodsResponse;

public record Source(
    ServiceAccountToken ServiceAccountToken,
    ConfigMap ConfigMap,
    DownwardAPI DownwardAPI
);