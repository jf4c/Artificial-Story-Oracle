using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Pause;

public sealed record PauseCampaignCommand(Guid CurrentPlayerId, Guid CampaignId) : ICommand;

