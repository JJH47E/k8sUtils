using K8sUtils.Models.GetPodsResponse;

namespace K8sUtils.Events;

public record PodSelectedEvent(PodItem Pod);
