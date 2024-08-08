namespace K8sUtils.Events;

public record SelectedItemChangedEvent<TModel>(TModel Value);
