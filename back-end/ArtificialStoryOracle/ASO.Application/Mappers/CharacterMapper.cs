using ASO.Application.UseCases.Characters.Create;
using ASO.Domain.Game.Dtos.Character;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;

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
        List<Class> classes, 
        List<Expertise> expertises)
    {
        return new CreateCharacterDto
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Ancestry = ancestry,
            Expertises = expertises,
            Classes = classes
        };
    }
}