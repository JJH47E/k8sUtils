using K8sUtils.Controls;
using K8sUtils.Events;
using K8sUtils.ProcessHosts;
using Terminal.Gui;

namespace K8sUtils;

public class MainWindow : Window
{
    private readonly NamespaceInputDialog _namespaceDialog;
    private readonly IKubectlHost _kubectlHost;
    
    public MainWindow ()
    {
        _kubectlHost = new KubectlHost();
        _namespaceDialog = new NamespaceInputDialog();
        var containerFrame = new PodListFrame(_kubectlHost);
        var actionFrame = new PodActionFrame()
        {
            X = Pos.Right(containerFrame)
        };

        _namespaceDialog.NamespaceEntered += containerFrame.OnNamespaceEntered;
        _namespaceDialog.NamespaceEntered += OnNamespaceEntered;
        
        containerFrame.PodSelected += actionFrame.OnPodSelected;

        Add(containerFrame, actionFrame, _namespaceDialog);
    }
    
    public void OnNamespaceEntered(object? sender, NamespaceSelectedEvent e)
    {
        Remove(_namespaceDialog);
    } 
}