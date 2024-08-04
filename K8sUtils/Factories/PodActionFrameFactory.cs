using K8sUtils.Controls;
using K8sUtils.Models.GetPodsResponse;
using K8sUtils.Services;

namespace K8sUtils.Factories;

public class PodActionFrameFactory(IKubectlService kubectlService) : IPodActionFrameFactory
{
    public PodActionFrame Create(Item? pod)
    {
        return new PodActionFrame(pod, kubectlService);
    }
}