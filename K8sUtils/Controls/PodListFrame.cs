using K8sUtils.Events;
using K8sUtils.Exceptions;
using K8sUtils.Models.GetPodsResponse;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodListFrame : FrameView
{
    private readonly AsyncListView<PodItem> _podList;
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
        
        _podList = new AsyncListView<PodItem>(GetPodsAsync, OnError)
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };
        _podList.SelectedItemChanged += OnSelectedItemChanged;
        
        Add(_podList);
    }

    public void OnNamespaceEntered(object? sender, NamespaceSelectedEvent e)
    {
        _namespace = e.Namespace;
        Application.Invoke(_podList.SetSourceAsync);
    }

    private void OnSelectedItemChanged(object? sender, SelectedItemChangedEvent<PodItem> e)
    {
        PodSelected?.Invoke(this, new PodSelectedEvent(e.Value));
    }

    private async Task<IEnumerable<PodItem>> GetPodsAsync()
    {
        return await _kubectlService.GetPodsAsync(_namespace);
    }

    private void OnError(Exception ex)
    {
        if (ex is KubectlRuntimeException)
        {
            FatalError?.Invoke(this, new FatalErrorEvent(ex.Message));
        }

        throw ex;
    }
}