namespace K8sUtils.Models.GetPodsResponse;

public record Status(
    List<Condition> Conditions,
    List<ContainerStatus> ContainerStatuses,
    string HostIP,
    List<IP> HostIPs,
    PodStatus Phase,
    string PodIP,
    List<IP> PodIPs,
    string QosClass,
    DateTime StartTime
);