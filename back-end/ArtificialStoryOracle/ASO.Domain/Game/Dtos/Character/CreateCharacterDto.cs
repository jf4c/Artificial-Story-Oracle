using ASO.Domain.Game.Entities;
using ASO.Domain.Game.ValueObjects;

namespace ASO.Domain.Game.Dtos.Character;

public sealed record CreateCharacterDto
{
    public string Name { get; set; } = string.Empty;
    public Ancestry Ancestry { get; set; } = null!;
    public List<Skill> Skills { get; set; } = null!;
    public Class Classes { get; set; } = null!;
    public AttributeModifiers Modifiers { get; set; } = null!;
    public string Backstory { get; set; } = null!; 
}
