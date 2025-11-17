namespace ASO.Api.Inputs;

public sealed record GenerateCampaignStoryInput
{
    public required Guid CampaignId { get; init; }
}
