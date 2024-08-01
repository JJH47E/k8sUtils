using K8sUtils.Controls;
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
        var containerFrame = new ContainerFrame(_kubectlHost);

        _namespaceDialog.NamespaceEntered += containerFrame.OnNamespaceEntered;
        _namespaceDialog.NamespaceEntered += OnNamespaceEntered;

        Add(containerFrame, _namespaceDialog);
    }
    
    public void OnNamespaceEntered(object? sender, string data)
    {
        Remove(_namespaceDialog);
    } 
}