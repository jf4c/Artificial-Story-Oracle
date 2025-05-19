using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Ancestry : Entity
{

    #region Constructors
    
    private Ancestry()
    {
        Modifiers = null!;
    }
    
    private Ancestry(string name, string backstory, AttributeModifiers modifiers, float size, int displacement)
    {
        Name = name;
        Backstory = backstory;
        Modifiers = modifiers;
        Size = size;
        Displacement = displacement;
    }
    
    #endregion
    
    public static Ancestry Create(string name, string backstory, AttributeModifiers modifiers, float size, int displacement)
    {
        return new Ancestry(name, backstory, modifiers, size, displacement);
    }
    
    public string Name { get; } = string.Empty;
    public string Backstory { get; } = string.Empty;
    public AttributeModifiers Modifiers { get; }
    public float Size { get; }
    public int Displacement { get; }
}