using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Delete;

public sealed record DeleteCampaignCommand(Guid CurrentPlayerId, Guid CampaignId) : ICommand;

