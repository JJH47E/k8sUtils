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
    private ContextSwitcher _contextSwitcher;
    
    public MainWindow(IPodActionFrameFactory podActionFrameFactory, IKubectlService kubectlService)
    {
        // These two lines shouldn't be needed
        Application.QuitKey = Key.Esc;
        KeyBindings.Add(Application.QuitKey, Command.QuitToplevel);
        
        _podActionFrameFactory = podActionFrameFactory;
        
        _namespaceDialog = new NamespaceInputDialog();
        _contextSwitcher = new ContextSwitcher(kubectlService)
        {
            Height = Dim.Auto(),
            Width = Dim.Percent(25),
            X = 0,
            Y = 0
        };
        _podListFrame = new PodListFrame(kubectlService)
        {
            Y = Pos.Bottom(_contextSwitcher)
        };
        _podActionFrame = _podActionFrameFactory.Create(null);
        _podActionFrame.X = Pos.Right(_podListFrame);

        _namespaceDialog.NamespaceEntered += _podListFrame.OnNamespaceEntered;
        _namespaceDialog.NamespaceEntered += _contextSwitcher.OnNamespaceSelected;
        _namespaceDialog.NamespaceEntered += OnNamespaceEntered;

        _contextSwitcher.UpdateNamespace += OnUpdateNamespace;
        
        _podListFrame.PodSelected += OnPodSelected;
        _podListFrame.FatalError += OnFatalError;
        
        StatusBar = new StatusBar();
        StatusBar.Add (
            new Shortcut
            {
                Title = "Quit",
                Key = Application.QuitKey
            }
        );
        
        Add(_contextSwitcher, _podListFrame, _podActionFrame, _namespaceDialog, StatusBar);
    }

    private void OnUpdateNamespace(object? sender, EventArgs e)
    {
        if (_namespaceDialog.IsAdded) return;
        Add(_namespaceDialog);
    }
    
    private void OnNamespaceEntered(object? sender, NamespaceSelectedEvent e)
    {
        Remove(_namespaceDialog);
    }

    private void OnPodSelected(object? sender, PodSelectedEvent e)
    {
        Remove(_podActionFrame);
        _podActionFrame.Dispose();
        _podActionFrame = _podActionFrameFactory.Create(e.Pod);
        _podActionFrame.X = Pos.Right(_podListFrame);
        Add(_podActionFrame);
    }

    private void OnFatalError(object? sender, FatalErrorEvent e)
    {
        var fatalExceptionDialog = new FatalExceptionDialog(e.Message);
        Add(fatalExceptionDialog);
    }
}