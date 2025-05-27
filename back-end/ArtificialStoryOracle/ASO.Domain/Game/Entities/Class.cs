using System.Text.Json.Serialization;
using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Class : Entity
{
    
    #region Constructors
    
    private Class()
    {
        Statistics = null!;
        Name = null!;
        Description = null!;
        Characters = new();
    }
    
    private Class(string name, string description, int hp, int mp)
    {
        Name = name;
        Description = description;
        Statistics = Statistics.Create(hp, mp);
    }
    
    #endregion
    
    public static Class Create(string name, string description, int hp, int mp)
    {
        return new Class(name, description, hp, mp);
    }
    
    public string Name { get; }
    public string Description { get; }
    public Statistics Statistics { get; }
    public List<Character>? Characters { get; }
    
    /*
     * Id | Name | Description           | ManaPoints | HealthPoints | Level | Expertises           | Characters
     * 1 | Mage | A powerful spellcaster | 100       | 50            | 1     |[Fireball, Ice Spike] | [Character1, Character2]
     * 
     */
}