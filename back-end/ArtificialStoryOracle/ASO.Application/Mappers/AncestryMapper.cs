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
                Name = a.Name,
                Backstory = a.Backstory,
                ModStrength = a.Modifiers.ModStrength,
                ModDexterity = a.Modifiers.ModDexterity,
                ModConstitution = a.Modifiers.ModConstitution,
                ModIntelligence = a.Modifiers.ModIntelligence,
                ModWisdom = a.Modifiers.ModWisdom,
                ModCharisma = a.Modifiers.ModCharisma,
                Size = a.Size,
                Displacement = a.Displacement
            })
        };
    }
}