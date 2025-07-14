using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Skill : Entity
{
    #region Constructors
    
    private Skill()
    {
        Name = null!; 
    }
    
    private Skill(string name, AttributeBase keyAttributes, bool trained = false, bool armorPenalty = false)
    {
        Name = name;
        KeyAttributes = keyAttributes;
        Trained = trained;
        ArmorPenalty = armorPenalty;
    }
    
    #endregion
    
    public static Skill Create(string name, AttributeBase keyAttributes, bool trained = false, bool armorPenalty = false)
    {
        return new Skill(name, keyAttributes, trained, armorPenalty);
    }
    
    public string Name { get; }
    public AttributeBase KeyAttributes { get; }
    public bool Trained { get; }
    public bool ArmorPenalty { get; }
    public List<Character>? Characters { get; set; }

}