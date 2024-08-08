namespace K8sUtils.Models.GetPodsResponse;

public record Terminated(
    string ContainerID,
    int ExitCode,
    DateTime FinishedAt,
    string Reason,
    DateTime StartedAt
);