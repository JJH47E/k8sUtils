namespace K8sUtils.Models.GetPodsResponse;

public record Root(
    string ApiVersion,
    List<Item> Items,
    string Kind,
    Metadata Metadata
);

public record Item(
    string ApiVersion,
    string Kind,
    Metadata Metadata,
    Spec Spec,
    Status Status
)
{
    public sealed override string ToString() => Metadata.Name;
    public string? GetServiceName() => Metadata.Labels.GetValueOrDefault("app");
    public string GetNamespace() => Metadata.Namespace;
    public string? GetCommitHash() => Metadata.Labels.GetValueOrDefault("pod-template-hash");
    // Ideally parse as an enum in future
    public string? GetStatus() => Status.Phase;
};

public record Metadata(
    DateTime CreationTimestamp,
    string GenerateName,
    Dictionary<string, string> Labels,
    string Name,
    string Namespace,
    List<OwnerReference> OwnerReferences,
    string ResourceVersion,
    string Uid
);

public record OwnerReference(
    string ApiVersion,
    bool BlockOwnerDeletion,
    bool Controller,
    string Kind,
    string Name,
    string Uid
);

public record Spec(
    List<Container> Containers,
    string DnsPolicy,
    bool EnableServiceLinks,
    string NodeName,
    string PreemptionPolicy,
    int Priority,
    string RestartPolicy,
    string SchedulerName,
    Dictionary<string, object> SecurityContext,
    string ServiceAccount,
    string ServiceAccountName,
    int TerminationGracePeriodSeconds,
    List<Toleration> Tolerations,
    List<Volume> Volumes
);

public record Container(
    string Image,
    string ImagePullPolicy,
    string Name,
    Dictionary<string, object> Resources,
    string TerminationMessagePath,
    string TerminationMessagePolicy,
    List<VolumeMount> VolumeMounts
);

public record VolumeMount(
    string MountPath,
    string Name,
    bool ReadOnly
);

public record Toleration(
    string Effect,
    string Key,
    string Operator,
    int TolerationSeconds
);

public record Volume(
    string Name,
    Projected Projected
);

public record Projected(
    int DefaultMode,
    List<Source> Sources
);

public record Source(
    ServiceAccountToken ServiceAccountToken,
    ConfigMap ConfigMap,
    DownwardAPI DownwardAPI
);

public record ServiceAccountToken(
    int ExpirationSeconds,
    string Path
);

public record ConfigMap(
    List<ConfigMapItem> Items,
    string Name
);

public record ConfigMapItem(
    string Key,
    string Path
);

public record DownwardAPI(
    List<DownwardAPIItem> Items
);

public record DownwardAPIItem(
    FieldRef FieldRef,
    string Path
);

public record FieldRef(
    string ApiVersion,
    string FieldPath
);

public record Status(
    List<Condition> Conditions,
    List<ContainerStatus> ContainerStatuses,
    string HostIP,
    List<IP> HostIPs,
    string Phase,
    string PodIP,
    List<IP> PodIPs,
    string QosClass,
    DateTime StartTime
);

public record Condition(
    DateTime? LastProbeTime,
    DateTime LastTransitionTime,
    string Status,
    string Type
);

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

public record LastState(
    Terminated Terminated
);

public record Terminated(
    string ContainerID,
    int ExitCode,
    DateTime FinishedAt,
    string Reason,
    DateTime StartedAt
);

public record State(
    Running Running
);

public record Running(
    DateTime StartedAt
);

public record IP(
    string Ip
);