using System.Collections.ObjectModel;
using K8sUtils.Exceptions;
using K8sUtils.ProcessHosts;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class ContainerFrame : FrameView
{
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly ContainerList _containerList;
    private readonly IKubectlHost _kubectlHost;

    private static string _namespace;
    
    public ContainerFrame(IKubectlHost kubectlHost)
    {
        _kubectlHost = kubectlHost;
        
        Title = "Containers";
        Width = Dim.Fill();
        Height = Dim.Fill();
        X = 0;
        Y = 0;
        
        _containerList = new ContainerList();
        _containerList.SetSource(new ObservableCollection<string>([]));
        
        Add(_containerList);
    }

    public void OnNamespaceEntered(object? sender, string data)
    {
        _namespace = data;
        Application.Invoke(CallGetContainersAsync);
    }

    private async void CallGetContainersAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource ();
        _containerList.Source = null;

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
                var fatalExceptionDialog = new FatalExceptionDialog(ex);
                Add(fatalExceptionDialog);
                return;
            }
            
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                await _containerList.SetSourceAsync(items);
            }
        }
        catch (OperationCanceledException _) {}
    }

    private async Task<ObservableCollection<string>> GetContainersAsync()
    {
        return await Task.FromResult(new ObservableCollection<string>(_kubectlHost.ListPods(_namespace)));
    }
}