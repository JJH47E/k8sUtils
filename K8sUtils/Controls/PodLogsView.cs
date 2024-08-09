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
        
        _logList.KeyUp += OnKeyUp;
        _logList.SelectedItemChanged += OnSelectedItemChange;
        
        Add(_logList);
    }

    public void UpdateView()
    {
        Application.Invoke(_logList.SetSourceAsync);
    }

    private void OnKeyUp(object? sender, Key @event)
    {
        // Press `C` to copy selected log
        if (@event.KeyCode == Key.C)
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