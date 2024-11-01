using K8sUtils.Events;
using K8sUtils.Models.GetPodsResponse;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodActionFrame : FrameView
{
    private readonly PodItem? _pod;
    private readonly IKubectlService _kubectlService;
    
    public EventHandler<FatalErrorEvent>? FatalError;
    
    public PodActionFrame(PodItem? pod, IKubectlService kubectlService)
    {
        _pod = pod;
        _kubectlService = kubectlService;
        
        Title = _pod?.ToString() ?? "Pod";
        Width = Dim.Fill();
        Height = Dim.Fill(1);

        if (pod is null)
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
            View = CreateLogsFrame()
        }, false);

        tabView.AddTab(new Tab()
        {
            DisplayText = "Commands",
            View = CreateCommandsFrame()
        }, false);

        tabView.SelectedTabChanged += OnTabChange;
        
        Add(tabView);
    }

    private void OnTabChange(object? sender, TabChangedEventArgs e)
    {
        if (e.NewTab.View is PodLogsView logsView)
        {
            if (_pod != null)
            {
                logsView.UpdateView();   
            }
        }
    }

    private PodLogsView CreateLogsFrame()
    {
        if (_pod is null)
        {
            throw new InvalidOperationException("Cannot get logs of unknown pod!");
        }
        
        var view = new PodLogsView(_pod.Namespace, _pod.ToString(), _kubectlService);
        view.FatalError += OnFatalError;
        return view;
    }

    private PodCommandsFrame CreateCommandsFrame()
    {
        if (_pod is null)
        {
            throw new InvalidOperationException("Cannot show useful commands of unknown pod");
        }

        var view = new PodCommandsFrame(_pod.Namespace, _pod.ToString());
        view.FatalError += OnFatalError;
        return view;
    }

    private PodInfoView CreateInfoView()
    {
        return new PodInfoView(_pod!);
    }

    private void OnFatalError(object? sender, FatalErrorEvent e)
    {
        FatalError?.Invoke(sender, e);
    }
}