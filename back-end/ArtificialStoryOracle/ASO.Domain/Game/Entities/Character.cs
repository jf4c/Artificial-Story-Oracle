using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using ASO.Domain.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Character : Entity
{
    private Character(Name name, Class @class, List<Expertise> expertises)
    {
        Name = name;
        Class = @class;
        Expertises = expertises;
        Ancestry = Ancestry.Create("Default", "Default");
    }
    
    public static Character Create(Name name, Class @class, List<Expertise> expertises)
        => new(name, @class, expertises);
    
    public Name Name { get; }
    public TypeCharacter TypeCharacter { get; }
    public Ancestry Ancestry { get; } 
    public Class Class { get; } 
    public List<Expertise> Expertises { get; set; }
    
    public int Level { get; set; } = 1;
    
    /*
     * id | name | TypeCharacter |  Backstory | Ancestry | Class |
     *  1 |  John | Player       |   ...      | elf      | mage |
     *  2 |  Mary | Npc          |   ...      | human    | warrior |
     *  3 |  Bob  | Npc          |   ...      | orc      | rogue |
     */
} 