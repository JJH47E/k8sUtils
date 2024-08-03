using Terminal.Gui;

namespace K8sUtils;

public class ConsoleRunner
{
    private readonly MainWindow _mainWindow;
    
    public ConsoleRunner(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public void Run()
    {
        Application.Init();
        _mainWindow.ColorScheme = Colors.ColorSchemes["Base"];
        Application.Run(_mainWindow);
        Application.Shutdown();
    }
}