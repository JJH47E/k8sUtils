namespace K8sUtils.Models.GetPodsResponse;

public record ContainerStatus(
    string ContainerID,
    string Image,
    string ImageID,
    LastState LastState,
    string Name,
    bool Ready,
    int RestartCount,
    bool Started,
    State State
);