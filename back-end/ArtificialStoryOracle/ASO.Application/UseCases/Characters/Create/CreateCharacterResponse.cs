using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game;
using ASO.Domain.Game.Enums;
using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Application.UseCases.Characters.Create;

public sealed record CreateCharacterResponse : IResponse
{ 
    public string Name { get; set; } = string.Empty;
    public string TypeCharacter { get; set; } = string.Empty;
    public int? Level { get; set; }
}

// public sealed record AncestryDto
// {
//     public Guid Id { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public string Backstory { get; set; } = string.Empty;
//     public AttributeModifiers Modifiers { get; set; } = null!;
//     public float Size { get; set; }
//     public int Displacement { get; set; }
// }
//
// public sealed record ClassDto
// {
//     public Guid Id { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public string Description { get; set; } = string.Empty;
//     public Statistics Statistics { get; set; } = null!;
// }
//
// public sealed record ExpertiseDto
// {
//     public Guid Id { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public string KeyAttributes { get; set; } = string.Empty;
//     public bool Trained { get; set; } = false;
//     public bool ArmorPenalty { get; set; } = false;
// }