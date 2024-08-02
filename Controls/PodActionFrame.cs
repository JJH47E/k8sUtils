using K8sUtils.Events;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodActionFrame : FrameView
{
    private readonly Label _label;
    
    public PodActionFrame()
    {
        Title = "Pod";
        Width = Dim.Fill();
        Height = Dim.Fill();

        _label = new Label()
        {
            Text = "No Pod Selected",
            X = Pos.Center(),
            Y = Pos.Center()
        };
        
        Add(_label);
    }

    public void OnPodSelected(object? sender, PodSelectedEvent @event)
    {
        _label.Text = @event.PodName;
    }
}