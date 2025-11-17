using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.SetStory;

public sealed record SetCampaignStoryCommand(
    Guid CampaignId,
    Guid CurrentPlayerId,
    string StoryIntroduction) : ICommand;
