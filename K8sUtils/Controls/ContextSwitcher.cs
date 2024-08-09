using K8sUtils.Services;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class ContextSwitcher : FrameView
{
    private readonly IKubectlService _kubectlService;
    private readonly Label _contextLabel;
    
    public ContextSwitcher(IKubectlService kubectlService)
    {
        _kubectlService = kubectlService;

        Title = "Context";

        _contextLabel = new Label()
        {
            Width = Dim.Fill(),
        };
        
        Task.Run(SetContext);
        
        Add(_contextLabel);
    }

    private async Task SetContext()
    {
        var currentContext = await _kubectlService.GetCurrentContextAsync();
        _contextLabel.Text = currentContext;
    }
}