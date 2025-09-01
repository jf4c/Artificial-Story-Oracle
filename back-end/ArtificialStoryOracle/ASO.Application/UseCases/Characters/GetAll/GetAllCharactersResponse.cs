using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Characters.GetAll;

public sealed record GetAllCharactersResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Ancestry { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public TypeCharacter TypeCharacter { get; set; }
    public int Level { get; set; }
}