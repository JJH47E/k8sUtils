using System.Collections.ObjectModel;
using K8sUtils.Events;
using K8sUtils.Exceptions;
using K8sUtils.ProcessHosts;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodListFrame : FrameView
{
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly PodList _podList;
    private readonly IKubectlHost _kubectlHost;

    private static string _namespace = null!;
    
    public EventHandler<PodSelectedEvent>? PodSelected;
    public EventHandler<FatalErrorEvent>? FatalError;
    
    public PodListFrame(IKubectlHost kubectlHost)
    {
        _kubectlHost = kubectlHost;
        
        Title = "Pods";
        Width = Dim.Percent(25);
        Height = Dim.Fill();
        X = 0;
        Y = 0;
        
        _podList = new PodList();
        _podList.SetSource(new ObservableCollection<string>([]));

        _podList.SelectedItemChanged += OnSelectedItemChanged;
        
        Add(_podList);
    }

    public void OnNamespaceEntered(object? sender, NamespaceSelectedEvent e)
    {
        _namespace = e.Namespace;
        Application.Invoke(CallGetContainersAsync);
    }

    private void OnSelectedItemChanged(object? sender, ListViewItemEventArgs e)
    {
        PodSelected?.Invoke(this, new PodSelectedEvent(e.Value.ToString()!, _namespace));
    }

    private async void CallGetContainersAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource ();
        _podList.Source = null;

        try
        {
            if (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }

            ObservableCollection<string> items;

            try
            {
                items = await Task.Run(GetContainersAsync, _cancellationTokenSource.Token);
            }
            catch (KubectlRuntimeException ex)
            {
                FatalError?.Invoke(this, new FatalErrorEvent(ex.Message));
                return;
            }

            if (items.Count == 0)
            {
                FatalError?.Invoke(this, 
                    new FatalErrorEvent($"Unable to find any pods in the namespace: {_namespace}. Is it correct?"));
                return;
            }
            
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                await _podList.SetSourceAsync(items);
            }
        }
        catch (OperationCanceledException _) {}
    }

    private async Task<ObservableCollection<string>> GetContainersAsync()
    {
        return await Task.FromResult(new ObservableCollection<string>(_kubectlHost.ListPods(_namespace)));
    }
}