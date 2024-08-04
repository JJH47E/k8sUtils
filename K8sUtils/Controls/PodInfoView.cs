using K8sUtils.Models.GetPodsResponse;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodInfoView : View
{
    public PodInfoView(Item pod)
    {
        Width = Dim.Fill();
        Height = Dim.Fill();

        var podLabel = new Label()
        {
            Text = $"Pod: {pod}",
        };

        var statusLabel = new Label()
        {
            Text = $"Status: {pod.GetStatus() ?? "Unknown"}",
            Y = Pos.Bottom(podLabel),
        };

        var podService = new Label()
        {
            Text = $"Service: {pod.GetServiceName() ?? "Unknown"}",
            Y = Pos.Bottom(statusLabel),
        };
        
        var namespaceLabel = new Label()
        {
            Text = $"Namespace: {pod.GetNamespace()}",
            Y = Pos.Bottom(podService),
        };

        var commitLabel = new Label()
        {
            Text = $"Commit: {pod.GetCommitHash() ?? "Unknown"}",
            Y = Pos.Bottom(namespaceLabel),
        };

        Add(podLabel, statusLabel, podService, namespaceLabel, commitLabel);
    }
}