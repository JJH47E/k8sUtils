using K8sUtils.Events;
using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class ContextSwitcher : FrameView
{
    private readonly IKubectlService _kubectlService;
    private readonly Label _contextLabel;
    private readonly Label _namespaceLabel;
    private readonly Button _updateButton;

    public EventHandler<EventArgs> UpdateNamespace;
    
    public ContextSwitcher(IKubectlService kubectlService)
    {
        _kubectlService = kubectlService;

        Title = "Context";

        _contextLabel = new Label()
        {
            Text = "Context:",
            Width = Dim.Fill(),
        };

        _namespaceLabel = new Label()
        {
            Text = "Namespace:",
            Width = Dim.Percent(75),
            Y = Pos.Bottom(_contextLabel),
        };

        _updateButton = new Button()
        {
            Text = "Update",
            Y = Pos.Bottom(_contextLabel),
            X = Pos.Right(_namespaceLabel),
            Width = Dim.Fill(),
        };

        _updateButton.Accept += (s, e) =>
        {
            e.Handled = true;
            UpdateNamespace?.Invoke(this, EventArgs.Empty);
        };
        
        Task.Run(SetContext);
        
        Add(_contextLabel, _namespaceLabel, _updateButton);
    }

    private async Task SetContext()
    {
        var currentContext = await _kubectlService.GetCurrentContextAsync();
        _contextLabel.Text = $"Context: {currentContext}";
    }

    public void OnNamespaceSelected(object? sender, NamespaceSelectedEvent @event)
    {
        _namespaceLabel.Text = $"Namespace: {@event.Namespace}";
    }
}