namespace ASO.Api.Inputs;

public record GenerateCharacterNamesInput
{
    public Guid? AncestryId { get; init; }
    public Guid? ClassId { get; init; }
}

