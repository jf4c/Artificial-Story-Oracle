namespace ASO.Api.Inputs;

public sealed record CreateGameInput
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}