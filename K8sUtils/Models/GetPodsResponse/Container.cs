namespace K8sUtils.Models.GetPodsResponse;

public record Container(
    string Image,
    string ImagePullPolicy,
    string Name,
    Dictionary<string, object> Resources,
    string TerminationMessagePath,
    string TerminationMessagePolicy,
    List<VolumeMount> VolumeMounts
);