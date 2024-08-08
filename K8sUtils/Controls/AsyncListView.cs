using System.Collections.ObjectModel;
using K8sUtils.Events;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class AsyncListView<TModel> : View
{
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly ListView _list;
    private readonly LoadingSpinner _spinner;
    private readonly Func<Task<IEnumerable<TModel>>> _getItems;
    private readonly Action<Exception>? _exceptionHandler;
    
    public EventHandler<FatalErrorEvent>? FatalError;
    public EventHandler<SelectedItemChangedEvent<TModel>>? SelectedItemChanged;
    
    public AsyncListView(Func<Task<IEnumerable<TModel>>> getItems, Action<Exception>? exceptionHandler = null)
    {
        _getItems = getItems;
        _exceptionHandler = exceptionHandler;
        _spinner = new LoadingSpinner();
        
        _list = new ListView()
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };

        _list.SelectedItemChanged += OnSelectedItemChanged;

        _list.SetSource(new ObservableCollection<TModel>());
        _list.Visible = false;

        _spinner.Visible = false;
        
        Add(_spinner, _list);
    }
    
    public async void SetSourceAsync()
    {
        var timer = new Timer (
            o => Application.Invoke(() => _spinner.AdvanceAnimation()),
            null,
            0,
            100
        );
        
        _cancellationTokenSource = new CancellationTokenSource();
        _list.Source = null;

        if (!_spinner.Visible)
        {
            _spinner.Visible = true;
        }

        try
        {
            if (_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }

            IEnumerable<TModel> items;

            try
            {
                items = await Task.Run(_getItems, _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                if (_exceptionHandler is null)
                {
                    throw;
                }

                _exceptionHandler(ex);
                return;
            }

            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                await _list.SetSourceAsync(new ObservableCollection<TModel>(items));

                if (_spinner.Visible)
                {
                    _spinner.Visible = false;
                }

                if (!_list.Visible)
                {
                    _list.Visible = true;
                }
            }
        }
        catch (OperationCanceledException _)
        {
        }
        finally
        {
            await timer.DisposeAsync();
        }
    }

    private void OnSelectedItemChanged(object? sender, ListViewItemEventArgs e)
    {
        SelectedItemChanged?.Invoke(sender, new SelectedItemChangedEvent<TModel>((TModel)e.Value));
    }
}