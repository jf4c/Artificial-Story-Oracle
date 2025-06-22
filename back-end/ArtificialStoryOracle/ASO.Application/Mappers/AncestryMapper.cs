using ASO.Application.UseCases.Ancestry.GetAllAncestry;
using ASO.Domain.Game.Entities;

namespace ASO.Application.Mappers;

public static class AncestryMapper
{
    public static GetAllAncestryResponse ToGetAllAncestryResponse(this IEnumerable<Ancestry> ancestries)
    {
        return new GetAllAncestryResponse
        {
            Ancestries = ancestries.Select(a => new AncestryDto
            {
                Id = a.Id,
                Name = a.Name,
            })
        };
    }
}