using Terminal.Gui;

namespace K8sUtils.Controls;

public class FatalExceptionDialog : Dialog
{
    public FatalExceptionDialog(string message)
    {
        Title = "Fatal Exception";
        
        var dialogLabel = new Label
        {
            Text = message,
            X = Pos.Center(),
            Y = Pos.Center()
        };
        
        var closeButton = new Button
        {
            Text = "Quit",
            Y = Pos.AnchorEnd(),
            X = Pos.Center()
        };
        closeButton.MouseClick += (_, _) =>
        {
            Application.Shutdown();
            Environment.Exit(1);
        };

        Add(dialogLabel, closeButton);
    }
}