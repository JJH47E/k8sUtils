namespace K8sUtils.Models.GetPodsResponse;

public record ServiceAccountToken(
    int ExpirationSeconds,
    string Path
);