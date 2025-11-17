using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Create;

public sealed record CreateCampaignParticipantDto
{
    public required Guid PlayerId { get; init; }
    public required Guid CharacterId { get; init; }
}

public sealed record CreateCampaignCommand(
    Guid CurrentPlayerId,
    string Name,
    string? Description,
    int MaxPlayers,
    bool IsPublic,
    string? StoryIntroduction,
    List<CreateCampaignParticipantDto> Participants) : ICommand;

