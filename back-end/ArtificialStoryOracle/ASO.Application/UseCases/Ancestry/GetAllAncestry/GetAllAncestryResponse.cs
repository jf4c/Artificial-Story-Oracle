using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Ancestry.GetAllAncestry;

public sealed record GetAllAncestryResponse : IResponse
{
    public IEnumerable<AncestryDto> Ancestries { get; init; } = new List<AncestryDto>();
}

public sealed record AncestryDto
{
    public string Name { get; init; } = string.Empty;
    public string Backstory { get; init; } = string.Empty;
    public int ModStrength { get; init; }
    public int ModDexterity { get; init; }
    public int ModConstitution { get; init; }
    public int ModIntelligence { get; init; }
    public int ModWisdom { get; init; }
    public int ModCharisma { get; init; }
    public float Size { get; init; }
    public int Displacement { get; init; }
};