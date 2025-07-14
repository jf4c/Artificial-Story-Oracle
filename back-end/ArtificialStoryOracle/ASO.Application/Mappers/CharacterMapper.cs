using ASO.Application.UseCases.Characters.Create;
using ASO.Domain.Game.Dtos.Character;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using ASO.Domain.Game.ValueObjects;

namespace ASO.Application.Mappers;

public static class CharacterMapper
{
    public static CreateCharacterResponse ToCreateCharacterResponse(this Character entity)
    {
        return new CreateCharacterResponse
        {
            Name = entity.Name,
            TypeCharacter = nameof(TypeCharacter.Player),
            Level = entity.Level,
        };
    }

    public static CreateCharacterDto ToCreateCharacterDto(this CreateCharacterCommand command,
        Ancestry ancestry,
        Class classe,
        List<Skill> skills, 
        AttributeModifiers modifiers)
    {
        return new CreateCharacterDto
        {
            Name = command.Name,
            Ancestry = ancestry,
            Skills = skills,
            Classes = classe,
            Modifiers = modifiers
        };
    }
}