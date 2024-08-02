using K8sUtils.Events;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodActionFrame : FrameView
{
    private TabView _tabView;
    
    public PodActionFrame()
    {
        Title = "Pod";
        Width = Dim.Fill();
        Height = Dim.Fill();

        UpdateTabView();
        
        Add(_tabView);
    }

    private void UpdateTabView()
    {
        _tabView = new TabView();
        
        _tabView.AddTab(new Tab()
        {
            DisplayText = "Logs",
            View = null
        }, true);
    }

    public void OnPodSelected(object? sender, PodSelectedEvent @event)
    {
        // set logs here asynchronously & pass into tab view
    }
}