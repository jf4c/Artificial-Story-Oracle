using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.ValueObjects;

public record Attribute : ValueObject
{
    private Attribute(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        Strength = strength;
        Dexterity = dexterity;
        Constitution = constitution;
        Intelligence = intelligence;
        Wisdom = wisdom;
        Charisma = charisma;
    }
    
    public static Attribute Create(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        return new Attribute(strength, dexterity, constitution, intelligence, wisdom, charisma);
    }

    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
}