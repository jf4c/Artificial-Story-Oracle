namespace ASO.Api.Inputs;

public sealed record CreateCampaignInput
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public int MaxPlayers { get; init; } = 6;
    public bool IsPublic { get; init; } = false;
}

