using K8sUtils.Controls;
using K8sUtils.Events;
using K8sUtils.ProcessHosts;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils;

public class MainWindow : Window
{
    private readonly NamespaceInputDialog _namespaceDialog;
    private readonly IKubectlHost _kubectlHost;
    private readonly IKubectlService _kubectlService;
    private PodActionFrame _podActionFrame;
    private PodListFrame _podListFrame;
    
    public MainWindow ()
    {
        _kubectlHost = new KubectlHost();
        _kubectlService = new KubectlService(_kubectlHost);
        _namespaceDialog = new NamespaceInputDialog();
        _podListFrame = new PodListFrame(_kubectlService);
        _podActionFrame = new PodActionFrame(null, null, _kubectlService)
        {
            X = Pos.Right(_podListFrame)
        };

        _namespaceDialog.NamespaceEntered += _podListFrame.OnNamespaceEntered;
        _namespaceDialog.NamespaceEntered += OnNamespaceEntered;
        
        _podListFrame.PodSelected += OnPodSelected;
        _podListFrame.FatalError += OnFatalError;

        Add(_podListFrame, _podActionFrame, _namespaceDialog);
    }
    
    private void OnNamespaceEntered(object? sender, NamespaceSelectedEvent e)
    {
        Remove(_namespaceDialog);
    }

    private void OnPodSelected(object? sender, PodSelectedEvent e)
    {
        Remove(_podActionFrame);
        _podActionFrame.Dispose();
        _podActionFrame = new PodActionFrame(e.PodName, e.Namespace, _kubectlService)
        {
            X = Pos.Right(_podListFrame)
        };
        Add(_podActionFrame);
    }

    private void OnFatalError(object? sender, FatalErrorEvent e)
    {
        var fatalExceptionDialog = new FatalExceptionDialog(e.Message);
        Add(fatalExceptionDialog);
    }
}