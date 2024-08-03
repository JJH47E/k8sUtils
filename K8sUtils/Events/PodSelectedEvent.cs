namespace K8sUtils.Events;

public record PodSelectedEvent(string PodName, string Namespace);
