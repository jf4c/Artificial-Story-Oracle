namespace ASO.Api.Inputs;

public sealed record CreateCampaignParticipantInput
{
    public required Guid PlayerId { get; init; }
    public required Guid CharacterId { get; init; }
}

public sealed record CreateCampaignInput
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public int MaxPlayers { get; init; } = 6;
    public bool IsPublic { get; init; } = false;
    public string? StoryIntroduction { get; init; }
    public required List<CreateCampaignParticipantInput> Participants { get; init; } = new();
}

