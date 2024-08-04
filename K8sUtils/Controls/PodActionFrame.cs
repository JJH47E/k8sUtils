using K8sUtils.Events;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodActionFrame : FrameView
{
    private readonly string? _podName;
    private readonly string? _namespace;
    private readonly IKubectlService _kubectlService;
    
    public EventHandler<FatalErrorEvent>? FatalError;
    
    public PodActionFrame(string? podName, string? @namespace, IKubectlService kubectlService)
    {
        _podName = podName;
        _namespace = @namespace;
        _kubectlService = kubectlService;
        
        Title = _podName ?? "Pod";
        Width = Dim.Fill();
        Height = Dim.Fill();

        if (string.IsNullOrWhiteSpace(_podName) || string.IsNullOrWhiteSpace(_namespace))
        {
            var label = new Label()
            {
                Text = "Select a pod to begin.",
                X = Pos.Center(),
                Y = Pos.Center()
            };

            Add(label);
            return;
        }

        var tabView = new TabView()
        {
            Height = Dim.Fill(),
            Width = Dim.Fill(),
        };
        
        tabView.AddTab(new Tab()
        {
            DisplayText = "Info",
            View = CreateInfoView()
        }, true);
        
        tabView.AddTab(new Tab()
        {
            DisplayText = "Logs",
            View = CreateLogsView()
        }, false);
        
        Add(tabView);
    }

    private LogsView CreateLogsView()
    {
        // create view & invoke async setter function
        var view = new LogsView(_kubectlService);
        view.FatalError += OnFatalError;
        view.UpdateView(_podName!, _namespace!);
        return view;
    }

    private PodInfoView CreateInfoView()
    {
        return new PodInfoView();
    }

    private void OnFatalError(object? sender, FatalErrorEvent e)
    {
        FatalError?.Invoke(sender, e);
    }
}