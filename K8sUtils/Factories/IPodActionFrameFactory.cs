using K8sUtils.Controls;
using K8sUtils.Models.GetPodsResponse;

namespace K8sUtils.Factories;

public interface IPodActionFrameFactory
{
    PodActionFrame Create(PodItem? pod);
}