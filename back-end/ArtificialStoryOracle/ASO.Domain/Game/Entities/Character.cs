using ASO.Domain.Game.Dtos.Character;
using ASO.Domain.Game.Enums;
using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Character : Entity, IAggragateRoot
{
    #region Constructors

    private Character()
    {
        Modifiers = null!;
        Name = null!;
        Ancestry = null!;
        Classes = new();
        Skills = new();
    }

    private Character(string name, Ancestry ancestry, List<Skill> skills, Class classe,
        AttributeModifiers modifiers, string? backstory)
    {
        Name = name;
        Ancestry = ancestry;
        Classes = [classe];
        Skills = skills;
        Modifiers = modifiers;
        Backstory = backstory;
        InitLevel();
    }

    #endregion

    public static Character Create(CreateCharacterDto dto)
    { 
        if (dto.Skills == null || dto.Skills.Count == 0)
            throw new ArgumentException("Expertises cannot be null or empty.", nameof(dto.Skills));

        if (dto.Classes == null)
            throw new ArgumentException("Classes cannot be null or empty.", nameof(dto.Classes));

        if (dto.Ancestry == null)
            throw new ArgumentException("Ancestry cannot be null.", nameof(dto.Ancestry));

        return new Character(dto.Name, dto.Ancestry, dto.Skills, dto.Classes, dto.Modifiers,
            dto.Backstory);
    }

    public string Name { get; }
    public TypeCharacter TypeCharacter { get; } = TypeCharacter.Player;
    public Ancestry Ancestry { get; }
    public List<Class>? Classes { get; }
    public List<Skill>? Skills { get; }
    public AttributeModifiers Modifiers { get; }
    public int Level { get; set; }
    public string? Backstory { get; set; }

    private void InitLevel()
    {
        Level = 1;
    }
}