using ASO.Domain.Game.Enums;

namespace ASO.Api.Inputs;

public sealed record UpdateCampaignInput
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required int MaxPlayers { get; init; }
    public required CampaignStatus Status { get; init; }
}

