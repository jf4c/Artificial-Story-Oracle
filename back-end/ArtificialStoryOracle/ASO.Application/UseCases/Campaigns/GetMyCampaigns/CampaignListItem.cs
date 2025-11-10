using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Campaigns.GetMyCampaigns;

public sealed record CampaignListItem : IResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required CampaignStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required int ParticipantsCount { get; init; }
    public required int MaxPlayers { get; init; }
    public required string MyRole { get; init; }
    public required bool IsCreator { get; init; }
}

