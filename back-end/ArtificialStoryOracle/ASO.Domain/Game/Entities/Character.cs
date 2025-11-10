﻿using ASO.Domain.Game.Dtos.Character;
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
        Backstory = null;
        Image = null!;
        ImageId = null!;
        AncestryId = Guid.Empty;
    }

    private Character(string name, Ancestry ancestry, List<Skill> skills, Class classe,
        AttributeModifiers modifiers, string? backstory, Image image)
    {
        Name = name;
        // Ancestry = ancestry;
        Classes = [classe];
        Skills = skills;
        Modifiers = modifiers;
        Backstory = backstory;
        // Image = image;
        ImageId = image.Id;
        AncestryId = ancestry.Id;
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

        if(dto.Image == null)
            throw new ArgumentException("Image cannot be null.", nameof(dto.Image));
            
        
        return new Character(dto.Name, dto.Ancestry, dto.Skills, dto.Classes, dto.Modifiers,
            dto.Backstory, dto.Image);
    }

    public string Name { get; }
    public TypeCharacter TypeCharacter { get; } = TypeCharacter.Player;
    public Ancestry Ancestry { get; }
    public Guid AncestryId { get; }
    public List<Class>? Classes { get; }
    public List<Skill>? Skills { get; }
    public AttributeModifiers Modifiers { get; }
    public int Level { get; set; }
    public string? Backstory { get; }
    public Image? Image { get; }
    public Guid? ImageId { get; }
    
    public ICollection<CampaignParticipant> CampaignParticipations { get; } = new List<CampaignParticipant>();

    private void InitLevel()
    {
        Level = 1;
    }
}