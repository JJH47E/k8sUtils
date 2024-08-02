using Terminal.Gui;

namespace K8sUtils.Controls;

public class FatalExceptionDialog : Dialog
{
    public FatalExceptionDialog(Exception exception)
    {
        Title = "Fatal Exception";
        
        var dialogLabel = new Label
        {
            Text = exception.Message,
            X = Pos.Center(),
            Y = Pos.Center()
        };
        
        var closeButton = new Button
        {
            Text = "Quit",
            Y = Pos.AnchorEnd(),
            X = Pos.Center()
        };
        closeButton.MouseClick += (_, _) => Environment.Exit(1);

        Add(dialogLabel, closeButton);
    }
}