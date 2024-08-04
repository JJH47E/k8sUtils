using System.Collections.ObjectModel;
using K8sUtils.Events;
using K8sUtils.Exceptions;
using K8sUtils.Services;
using Terminal.Gui;
using TextCopy;

namespace K8sUtils.Controls;

public class LogsView : ListView
{
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly IKubectlService _kubectlService;
    private string _namespace = null!;
    private string _podName = null!;
    
    public EventHandler<FatalErrorEvent>? FatalError;
    
    public LogsView(IKubectlService kubectlService)
    {
        _kubectlService = kubectlService;
        
        Width = Dim.Fill();
        Height = Dim.Fill();

        SelectedItemChanged += OnSelectedChanged;
    }

    private void OnSelectedChanged(object? sender, ListViewItemEventArgs e)
    {
        var content = e.Value.ToString();
        if (!string.IsNullOrWhiteSpace(content))
        {
            ClipboardService.SetText(content);   
        }
    }

    public void UpdateView(string podName, string @namespace)
    {
        _podName = podName;
        _namespace = @namespace;
        
        Application.Invoke(CallGetLogsAsync);
    }

    private async void CallGetLogsAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource ();
        Source = null;

        try
        {
            if (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }

            ObservableCollection<string> logs;

            try
            {
                logs = await Task.Run(GetLogsAsync, _cancellationTokenSource.Token);
            }
            catch (KubectlRuntimeException ex)
            {
                FatalError?.Invoke(this, new FatalErrorEvent(ex.Message));
                return;
            }
            
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                await SetSourceAsync(logs);
            }
        }
        catch (OperationCanceledException _) {}
    }

    private async Task<ObservableCollection<string>> GetLogsAsync()
    {
        return new ObservableCollection<string>(await _kubectlService.GetLogsAsync(_podName, _namespace));
    }
}