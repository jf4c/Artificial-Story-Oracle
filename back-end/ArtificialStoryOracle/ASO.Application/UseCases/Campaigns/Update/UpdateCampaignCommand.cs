using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Campaigns.Update;

public sealed record UpdateCampaignCommand(
    Guid CurrentPlayerId,
    Guid CampaignId,
    string Name,
    string? Description,
    int MaxPlayers,
    CampaignStatus Status) : ICommand;

