using K8sUtils.Events;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class NamespaceInputDialog : Dialog
{
    public event EventHandler<NamespaceSelectedEvent>? NamespaceEntered;
    
    public NamespaceInputDialog()
    {
        Title = "Input Namespace";
        var namespaceLabel = new Label { Text = "Namespace:" };

        var namespaceText = new TextField
        {
            X = Pos.Right(namespaceLabel) + 1,
            Width = Dim.Percent(50)
        };
        
        var btnSubmit = new Button
        {
            Text = "Submit",
            Y = Pos.Bottom(namespaceLabel) + 1,
            X = Pos.Center(),
            IsDefault = true
        };

        btnSubmit.Accept += (s, e) =>
        {
            if (!string.IsNullOrWhiteSpace(namespaceText.Text))
            {
                NamespaceEntered?.Invoke(this, new NamespaceSelectedEvent(namespaceText.Text));   
            }
        };

        Add(namespaceLabel, namespaceText, btnSubmit);
    }
}