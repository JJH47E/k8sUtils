namespace K8sUtils.Models.Context;

public class Context
{
    public string Name { get; init; } = null!;
    public bool IsCurrent { get; set; }
    
    public override string ToString() => Name;
}