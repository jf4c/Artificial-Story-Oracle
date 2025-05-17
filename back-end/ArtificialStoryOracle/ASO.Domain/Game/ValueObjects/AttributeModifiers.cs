using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.ValueObjects;

public record AttributeModifiers: ValueObject
{
    private AttributeModifiers(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
    {
        ModStrength = strength;
        ModDexterity = dexterity;
        ModConstitution = constitution;
        ModIntelligence = intelligence;
        ModWisdom = wisdom;
        ModCharisma = charisma;
    }
    
    public static AttributeModifiers Create(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma) =>
        new(strength, dexterity, constitution, intelligence, wisdom, charisma);

    public int ModStrength { get; }
    public int ModDexterity { get; }
    public int ModConstitution { get; }
    public int ModIntelligence { get; }
    public int ModWisdom { get; }
    public int ModCharisma { get; }
}