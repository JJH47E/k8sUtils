using K8sUtils.Events;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodActionFrame : FrameView
{
    private readonly Label _label;
    
    public PodActionFrame()
    {
        Title = "Pod";
        Width = Dim.Percent(50);
        Height = Dim.Fill();

        _label = new Label() { Text = "pick a pod to begin" };
        
        Add(_label);
    }

    public void OnPodSelected(object sender, PodSelectedEvent @event)
    {
        _label.Text = @event.PodName;
    }
}