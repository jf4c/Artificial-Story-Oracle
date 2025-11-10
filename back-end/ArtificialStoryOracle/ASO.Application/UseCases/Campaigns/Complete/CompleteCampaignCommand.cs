using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Complete;

public sealed record CompleteCampaignCommand(Guid CurrentPlayerId, Guid CampaignId) : ICommand;

