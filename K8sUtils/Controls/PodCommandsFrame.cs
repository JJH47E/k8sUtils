using K8sUtils.Events;
using K8sUtils.Mappers;
using Terminal.Gui;

namespace K8sUtils.Controls;

public class PodCommandsFrame : View
{
    private readonly string _namespace;
    private readonly string _podName;

    public EventHandler<FatalErrorEvent>? FatalError;

    public PodCommandsFrame(string @namespace, string podName)
    {
        _namespace = @namespace;
        _podName = podName;

        Width = Dim.Fill();
        Height = Dim.Fill();

        var portForwardCommand = new CopyText(
            "Port Forwarding",
            CommandMappers.GetPortForwardCommand(_namespace, _podName));

        var shellCommand = new CopyText(
            "Create Shell",
            CommandMappers.GetShellCommand(_namespace, _podName))
        {
            Y = Pos.Bottom(portForwardCommand)
        };

        var logsCommand = new CopyText(
            "Stream Logs",
            CommandMappers.GetStreamLogsCommand(_namespace, _podName))
        {
            Y = Pos.Bottom(shellCommand)
        };

        Add(portForwardCommand, shellCommand, logsCommand);
    }
}