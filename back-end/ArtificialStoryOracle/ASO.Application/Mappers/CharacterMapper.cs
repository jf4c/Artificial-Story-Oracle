using ASO.Application.Pagination;
using ASO.Application.UseCases.Characters.Create;
using ASO.Application.UseCases.Characters.GetAll;
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

    public static PaginatedResult<GetAllCharactersResponse> ToGetAllCharactersResponse(
        this PaginatedResult<Character> entities) =>
        new()
        {
            Results = entities.Results.Select(e => new GetAllCharactersResponse
            {
                Id = e.Id,
                Name = e.Name,
                Image = e.Image?.Url ?? string.Empty,
                Ancestry = e.Ancestry.Name,
                Class = e.Classes?.First().Name ?? string.Empty,
                Level = e.Level,
                
            }).ToList(),
            
            CurrentPage = entities.CurrentPage,
            PageCount = entities.PageCount,
            PageSize = entities.PageSize,
            RowCount = entities.RowCount,
        };

    public static CreateCharacterDto ToCreateCharacterDto(this CreateCharacterCommand command,
        Ancestry ancestry,
        Class classe,
        List<Skill> skills, 
        Image image)
    {
        return new CreateCharacterDto
        {
            Name = command.Name,
            PlayerId = command.PlayerId,
            Ancestry = ancestry,
            Skills = skills,
            Classes = classe,
            Modifiers = command.Modifiers,
            Image = image
            
        };
    }
    
    
}