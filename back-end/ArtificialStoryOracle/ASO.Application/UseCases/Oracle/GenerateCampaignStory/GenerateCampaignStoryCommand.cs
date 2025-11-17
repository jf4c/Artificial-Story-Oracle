using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle.GenerateCampaignStory;

public sealed record GenerateCampaignStoryCommand(Guid CampaignId) : ICommand;
