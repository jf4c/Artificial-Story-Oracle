using ASO.Application.UseCases.Characters.GetAll;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Repositories.Abstractions;

namespace ASO.Application.Builders;

public class GetPaginatedCharactersQueryBuilder : 
    QueryBuilderBase<GetPaginatedCharactersQueryBuilder, Character, GetAllCharactersFilter>
{
    public static GetPaginatedCharactersQueryBuilder CreateBuilder(ICharacterRepository characterRepository)
    {
        _instance = new()
        {
            Query = characterRepository.GetAll(),
        };
        return _instance;
    }
    
    public GetPaginatedCharactersQueryBuilder SetFilter(GetAllCharactersFilter filter)
    {
        _instance.Filter = filter;

        return this;
    }

    public GetPaginatedCharactersQueryBuilder FilterByName()
    {
        if (Filter.Name.Length > 0)
        {
            Query = Query.Where(c => c.Name.Contains(Filter.Name));
        }
        
        return this;
    }
}