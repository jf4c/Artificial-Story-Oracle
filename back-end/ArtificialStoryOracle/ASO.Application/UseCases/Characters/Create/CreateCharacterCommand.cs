using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.ValueObjects;

namespace ASO.Application.UseCases.Characters.Create;

public sealed record CreateCharacterCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public Guid AncestryId { get; set; }
    public List<Guid> SkillsIds { get; set; } = new();
    public Guid ClasseId { get; set; } = Guid.Empty;
    public string Backstory { get; set; } = string.Empty;
    public Guid ImageId { get; set; } = Guid.Empty;
    public AttributeModifiers Modifiers { get; set; } = null!;
}