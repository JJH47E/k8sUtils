namespace K8sUtils.Models.GetPodsResponse;

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