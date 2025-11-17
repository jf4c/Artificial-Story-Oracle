using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle.GenerateCampaignStory;

public sealed record GenerateCampaignStoryResponse : IResponse
{
    public required string Story { get; init; }
    public required string Prompt { get; init; }
}
