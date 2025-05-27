using ASO.Domain.Game.Dtos.Character;
using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Character : Entity, IAggragateRoot
{
    #region Constructors
    
    private Character() {
        Name = null!;
        Ancestry = null!;
        Classes = new();
        Expertises = new();
    }
    
    private Character(Name name, Ancestry ancestry, List<Expertise> expertises,  List<Class> classes)
    {
        Name = name;
        Ancestry = ancestry;
        Classes = classes;
        Expertises = expertises;
        InitLevel();
    }
    
    #endregion

    public static Character Create(CreateCharacterDto dto)
    {
        var name = Name.Create(dto.FirstName, dto.LastName);
        if (dto.Expertises == null || dto.Expertises.Count == 0)
            throw new ArgumentException("Expertises cannot be null or empty.", nameof(dto.Expertises));
        
        if (dto.Classes == null || dto.Classes.Count == 0)
            throw new ArgumentException("Classes cannot be null or empty.", nameof(dto.Classes));
        
        if (dto.Ancestry == null)
            throw new ArgumentException("Ancestry cannot be null.", nameof(dto.Ancestry));

        return new Character(name, dto.Ancestry, dto.Expertises, dto.Classes);
    }
    
    public Name Name { get; }
    public TypeCharacter TypeCharacter { get; } = TypeCharacter.Player;
    public Ancestry Ancestry { get; } 
    public List<Class>? Classes { get; } 
    public List<Expertise>? Expertises { get; }
    public int Level { get; set; }

    private void InitLevel()
    {
        Level = 1;
    }
} 