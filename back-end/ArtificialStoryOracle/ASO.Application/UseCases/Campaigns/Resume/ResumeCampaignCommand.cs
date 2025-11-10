using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.Resume;

public sealed record ResumeCampaignCommand(Guid CurrentPlayerId, Guid CampaignId) : ICommand;

