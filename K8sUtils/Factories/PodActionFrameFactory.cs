using K8sUtils.Controls;
using K8sUtils.Services;

namespace K8sUtils.Factories;

public class PodActionFrameFactory(IKubectlService kubectlService) : IPodActionFrameFactory
{
    public PodActionFrame Create(string? podName, string? @namespace)
    {
        return new PodActionFrame(podName, @namespace, kubectlService);
    }
}