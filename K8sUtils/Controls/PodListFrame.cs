using System.Collections.ObjectModel;
using K8sUtils.Events;
using K8sUtils.Exceptions;
using K8sUtils.Services;
using Terminal.Gui;
using GetPodsResponseItem = K8sUtils.Models.GetPodsResponse.Item;

namespace K8sUtils.Controls;

public class PodListFrame : FrameView
{
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly PodList _podList;
    private readonly IKubectlService _kubectlService;

    private static string _namespace = null!;
    
    public EventHandler<PodSelectedEvent>? PodSelected;
    public EventHandler<FatalErrorEvent>? FatalError;
    
    public PodListFrame(IKubectlService kubectlService)
    {
        _kubectlService = kubectlService;
        
        Title = "Pods";
        Width = Dim.Percent(25);
        Height = Dim.Fill(1);
        X = 0;
        Y = 0;
        
        _podList = new PodList();
        _podList.SetSource(new ObservableCollection<GetPodsResponseItem>([]));
        
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
        PodSelected?.Invoke(this, new PodSelectedEvent((GetPodsResponseItem)e.Value));
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

            ObservableCollection<GetPodsResponseItem> items;

            try
            {
                items = await Task.Run(GetPodsAsync, _cancellationTokenSource.Token);
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

    private async Task<ObservableCollection<GetPodsResponseItem>> GetPodsAsync()
    {
        return new ObservableCollection<GetPodsResponseItem>(await _kubectlService.GetPodsAsync(_namespace));
    }
}