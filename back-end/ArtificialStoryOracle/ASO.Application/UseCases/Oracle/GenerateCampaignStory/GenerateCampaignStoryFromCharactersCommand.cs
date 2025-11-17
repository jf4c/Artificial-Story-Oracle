using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle.GenerateCampaignStory;

public sealed record GenerateCampaignStoryFromCharactersCommand(
    List<Guid> CharacterIds,
    string? CampaignName = null,
    string? CampaignDescription = null
) : ICommand;
