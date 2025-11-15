using ASO.Application.Abstractions.Shared;
using ASO.Application.Pagination;

namespace ASO.Application.UseCases.Characters.GetAll;

public sealed record GetAllCharactersFilter : PaginatedQueryBase, IQuery 
{
    public string Name { get; set; } = string.Empty;
    public Guid? PlayerId { get; set; } = null;
    public Guid? AncestryId { get; set; } = null;
    public Guid? ClassId { get; set; } = null;
    public Guid? CampaignId { get; set; } = null;
}