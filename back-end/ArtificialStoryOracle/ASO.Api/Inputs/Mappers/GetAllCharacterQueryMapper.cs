using ASO.Application.UseCases.Characters.GetAll;

namespace ASO.Api.Inputs.Mappers;

public static class GetAllCharacterQueryMapper
{
    public static GetAllCharactersFilter ToFilter(this GetAllCharacterQuery query)
    {
        return new GetAllCharactersFilter
        {
            Name = query.Name,
            PlayerId = query.PlayerId,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}