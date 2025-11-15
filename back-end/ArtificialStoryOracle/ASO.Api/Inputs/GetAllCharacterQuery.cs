using ASO.Application.Pagination;

namespace ASO.Api.Inputs;

public sealed record GetAllCharacterQuery : PaginatedQueryBase
{
    public string Name { get; set; } = string.Empty;
    public Guid? PlayerId { get; set; } = null;
    public Guid? AncestryId { get; set; } = null;
    public Guid? ClassId { get; set; } = null;
    public Guid? CampaignId { get; set; } = null;
}