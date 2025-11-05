using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle.GenerateNames;

public record GenerateCharacterNamesResponse : IResponse
{
    public required List<string> MaleNames { get; init; }
    public required List<string> FemaleNames { get; init; }
    public string? Ancestry { get; init; }
    public string? Class { get; init; }
    public required DateTime GeneratedAt { get; init; }
}

