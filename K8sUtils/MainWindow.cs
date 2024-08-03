using K8sUtils.Controls;
using K8sUtils.Events;
using K8sUtils.Factories;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils;

public class MainWindow : Window
{
    private readonly NamespaceInputDialog _namespaceDialog;
    private readonly IPodActionFrameFactory _podActionFrameFactory;
    private PodActionFrame _podActionFrame;
    private PodListFrame _podListFrame;
    
    public MainWindow(IPodActionFrameFactory podActionFrameFactory, IKubectlService kubectlService)
    {
        _podActionFrameFactory = podActionFrameFactory;
        
        _namespaceDialog = new NamespaceInputDialog();
        _podListFrame = new PodListFrame(kubectlService);
        _podActionFrame = _podActionFrameFactory.Create(null, null);
        _podActionFrame.X = Pos.Right(_podListFrame);

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
        _podActionFrame = _podActionFrameFactory.Create(e.PodName, e.Namespace);
        _podActionFrame.X = Pos.Right(_podListFrame);
        Add(_podActionFrame);
    }

    private void OnFatalError(object? sender, FatalErrorEvent e)
    {
        var fatalExceptionDialog = new FatalExceptionDialog(e.Message);
        Add(fatalExceptionDialog);
    }
}