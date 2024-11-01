using K8sUtils.Events;
using K8sUtils.Exceptions;
using K8sUtils.Services;
using Terminal.Gui;
using TextCopy;

namespace K8sUtils.Controls;

public class PodLogsView : View
{
    private readonly IKubectlService _kubectlService;
    private readonly AsyncListView<string> _logList;
    private readonly string _namespace;
    private readonly string _podName;
    
    public EventHandler<FatalErrorEvent>? FatalError;

    private string SelectedLog { get; set; }
    
    public PodLogsView(string @namespace, string podName, IKubectlService kubectlService)
    {
        _kubectlService = kubectlService;
        _namespace = @namespace;
        _podName = podName;
        
        Width = Dim.Fill();
        Height = Dim.Fill();

        _logList = new AsyncListView<string>(GetLogsAsync, OnError)
        {
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        
        _logList.MouseClick += OnMouseClick;
        _logList.SelectedItemChanged += OnSelectedItemChange;
        
        Add(_logList);
    }

    public void UpdateView()
    {
        Application.Invoke(_logList.SetSourceAsync);
    }

    private void OnMouseClick(object? sender, MouseEventEventArgs @event)
    {
        if ((@event.MouseEvent.Flags & MouseFlags.Button3Clicked) != 0)
        {
            ClipboardService.SetText(SelectedLog);
            @event.Handled = true;
        }
    }

    private void OnSelectedItemChange(object? sender, SelectedItemChangedEvent<string> @event)
    {
        SelectedLog = @event.Value;
    }

    private async Task<IEnumerable<string>> GetLogsAsync()
    {
        return await _kubectlService.GetLogsAsync(_podName, _namespace);
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