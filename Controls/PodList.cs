using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodList : ListView
{
    public PodList()
    {
        X = 0;
        Y = 0;
        
        Width = Dim.Fill();
        Height = Dim.Fill();
    }
}