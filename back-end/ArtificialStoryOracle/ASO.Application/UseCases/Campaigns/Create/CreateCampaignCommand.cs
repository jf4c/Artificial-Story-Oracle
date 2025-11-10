using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Create;

public sealed record CreateCampaignCommand(
    Guid CurrentPlayerId,
    string Name,
    string? Description,
    int MaxPlayers,
    bool IsPublic) : ICommand;

