using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle.GenerateNames;

public record GenerateCharacterNamesCommand : ICommand
{
    public Guid? AncestryId { get; init; }
    public Guid? ClassId { get; init; }
}

