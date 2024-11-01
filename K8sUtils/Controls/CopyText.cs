using Terminal.Gui;
using TextCopy;

namespace K8sUtils.Controls;

public class CopyText : FrameView
{
    private readonly string _text;
    private readonly string _label;

    public CopyText(string label, string text)
    {
        _text = text;
        _label = label;
        Width = Dim.Fill();
        Height = Dim.Auto();

        Title = _label;

        var textLabel = new Label()
        {
            Text = _text,
            Width = Dim.Auto(),
        };

        var copyButton = new Button()
        {
            Text = "Copy",
            X = Pos.AnchorEnd(),
        };

        copyButton.Accept += (s, e) =>
        {
            e.Handled = true;
            ClipboardService.SetText(_text);
        };

        Add(textLabel, copyButton);
    }
}
