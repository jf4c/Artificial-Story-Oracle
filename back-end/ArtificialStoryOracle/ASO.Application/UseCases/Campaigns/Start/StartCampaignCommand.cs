using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Start;

public sealed record StartCampaignCommand(Guid CurrentPlayerId, Guid CampaignId) : ICommand;

