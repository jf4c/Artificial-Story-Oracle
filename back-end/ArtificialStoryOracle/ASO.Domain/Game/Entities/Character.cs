using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using ASO.Domain.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Character : Entity
{
    #region Constructors
    
    private Character() {
        Name = null!;
        Ancestry = null!;
        Classes = new();
        Expertises = new();
        Level = null;
    }
    
    private Character(Name name, Ancestry ancestry)
    {
        Name = name;
        Ancestry = ancestry;
    }
    
    #endregion
    
    public static Character Create(Name name, Ancestry ancestry)
        => new(name, ancestry);
    
    public Name Name { get; }
    public TypeCharacter TypeCharacter { get; } = TypeCharacter.Player;
    public Ancestry Ancestry { get; } 
    public List<Class>? Classes { get; } 
    public List<Expertise>? Expertises { get; }
    public int? Level { get; }
} 