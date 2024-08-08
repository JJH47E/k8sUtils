using Terminal.Gui;

namespace K8sUtils.Controls;

public class LoadingSpinner : SpinnerView
{
    public LoadingSpinner()
    {
        X = Pos.Center();
        Y = Pos.Center();

        Style = new SpinnerStyle.Dots();
        AutoSpin = true;
        Visible = true;
        SpinDelay = 1;
    }
}