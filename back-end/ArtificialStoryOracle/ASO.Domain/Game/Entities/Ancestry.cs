using ASO.Domain.Game.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Ancestry
{
    private Ancestry()
    {
        
    }
    
    public static Ancestry Create(string name, string description)
    {
        return new Ancestry();
    }
    
    public string Name { get; } = string.Empty;
    public string Backstory { get; } = string.Empty;
    public AttributeModifiers Modifiers { get; } = AttributeModifiers.Create(0, 0, 0, 0, 0, 0);
    public float Size { get; set; }
    public int Displacement { get; set; }
    
    /* 
     * name   |  Backstory | ModStrength | ModDexterity | ModConstitution | ModIntelligence | ModWisdom | ModCharisma
     * humano |  Human     | +1         | +0           | +0              | +0              | +0        | +0
     * elven  |  Elf       | +0         | +2           | -2              | +0              | +0        | +0
     * orc    |  Orc       | +2         | +0           | +0              | -2              | +0        | +0
     */
}