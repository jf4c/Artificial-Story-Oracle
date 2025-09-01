using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.Builders;
using ASO.Application.Extensions;
using ASO.Application.Mappers;
using ASO.Application.Pagination;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Repositories.Abstractions;

namespace ASO.Application.UseCases.Characters.GetAll;

public sealed class GetAllCharactersHandler(ICharacterRepository characterRepository)
    : IGetAllCharactersHandler
{
    private readonly ICharacterRepository _characterRepository = characterRepository;
    
    public async Task<PaginatedResult<GetAllCharactersResponse>> Handle(GetAllCharactersFilter filter)
    {
        var response = await GetAllCharactersByFilterAsync(filter);

        return response.ToGetAllCharactersResponse();
    }

    private async Task<PaginatedResult<Character>> GetAllCharactersByFilterAsync(GetAllCharactersFilter filter)
    {
        var query = GetPaginatedCharactersQueryBuilder
            .CreateBuilder(_characterRepository)
            .SetFilter(filter)
            .FilterByName()
            .SetOrderBy()
            .BuildQuery();

        return await query.GetPaginatedAsync(filter.Page, filter.PageSize);
    }
}