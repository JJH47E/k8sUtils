namespace K8sUtils.Models.GetPodsResponse;

public record DownwardAPI(
    List<DownwardAPIItem> Items
);