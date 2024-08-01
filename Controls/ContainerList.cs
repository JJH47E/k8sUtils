using Terminal.Gui;

namespace K8sUtils.Controls;

public class ContainerList : ListView
{
    public ContainerList()
    {
        X = 0;
        Y = 0;
        
        Width = Dim.Fill();
        Height = Dim.Fill();
    }
}