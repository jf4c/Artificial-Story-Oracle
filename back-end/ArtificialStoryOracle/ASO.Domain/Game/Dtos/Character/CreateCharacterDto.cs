using ASO.Domain.Game.Entities;

namespace ASO.Domain.Game.Dtos.Character;

public sealed record CreateCharacterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Ancestry Ancestry { get; set; } = null!;
    public List<Expertise> Expertises { get; set; } = null!;
    public List<Class> Classes { get; set; } = null!;
}
