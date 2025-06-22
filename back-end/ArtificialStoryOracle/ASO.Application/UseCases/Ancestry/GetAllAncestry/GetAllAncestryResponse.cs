using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Ancestry.GetAllAncestry;

public sealed record GetAllAncestryResponse : IResponse
{
    public IEnumerable<AncestryDto> Ancestries { get; init; } = new List<AncestryDto>();
}

public sealed record AncestryDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
};