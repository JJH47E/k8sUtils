using K8sUtils.Controls;

namespace K8sUtils.Factories;

public interface IPodActionFrameFactory
{
    PodActionFrame Create(string? podName, string? @namespace);
}