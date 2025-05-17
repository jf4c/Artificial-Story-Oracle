using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Expertise : Entity
{
    private Expertise(string name, AttributeBase keyAttributes, bool trained = false, bool armorPenalty = false)
    {
        Name = name;
        KeyAttributes = keyAttributes;
        Trained = trained;
        ArmorPenalty = armorPenalty;
    }
    
    public static Expertise Create(string name, AttributeBase keyAttributes, bool trained = false, bool armorPenalty = false)
    {
        return new Expertise(name, keyAttributes, trained, armorPenalty);
    }
    
    public string Name { get; }
    public AttributeBase KeyAttributes { get; }
    public bool Trained { get; }
    public bool ArmorPenalty { get; }

}