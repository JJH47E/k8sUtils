using System.Collections.ObjectModel;
using K8sUtils.Mappers;
using K8sUtils.Models.GetPodsResponse;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodInfoView : View
{
    public PodInfoView(PodItem pod)
    {
        Width = Dim.Fill();
        Height = Dim.Fill();

        var podLabel = new Label()
        {
            Text = $"Pod: {pod}",
        };

        var statusLabel = new Label()
        {
            Text = $"Status: {PodStatusMappers.StatusToIcon(pod.PodStatus ?? PodStatus.Unknown)}",
            Y = Pos.Bottom(podLabel),
        };
        
        var namespaceLabel = new Label()
        {
            Text = $"Namespace: {pod.Namespace}",
            Y = Pos.Bottom(statusLabel),
        };

        var createdLabel = new Label()
        {
            Text = $"Created: {pod.Created.ToString()}",
            Y = Pos.Bottom(namespaceLabel),
        };

        var labels = new ListWrapper<string>(
            new ObservableCollection<string>(
                pod.Labels.Select(x => $"{x.Key}: {x.Value}")));

        var labelsLabel = new Label()
        {
            Text = "Labels:",
            Y = Pos.Bottom(createdLabel),
        };

        var labelView = new ListView()
        {
            Y = Pos.Bottom(labelsLabel),
            Height = Dim.Fill(),
            Width = Dim.Fill(),
            Source = labels
        };

        Add(podLabel, statusLabel, namespaceLabel, createdLabel, labelsLabel, labelView);
    }
}