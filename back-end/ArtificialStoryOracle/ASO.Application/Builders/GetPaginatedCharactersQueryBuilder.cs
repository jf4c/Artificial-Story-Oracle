using ASO.Application.UseCases.Characters.GetAll;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;

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

        return _instance;
    }

    public GetPaginatedCharactersQueryBuilder FilterByName()
    {
        if (_instance.Filter?.Name?.Length > 0)
        {
            _instance.Query = _instance.Query.Where(c => c.Name.Contains(_instance.Filter.Name));
        }
        
        return _instance;
    }
    
    public GetPaginatedCharactersQueryBuilder FilterByPlayerId()
    {
        if (_instance.Query == null || _instance.Filter == null)
            throw new InvalidOperationException("Query ou Filter não inicializados no builder.");
        if (_instance.Filter.PlayerId.HasValue)
        {
            _instance.Query = _instance.Query.Where(c => c.PlayerId == _instance.Filter.PlayerId.Value);
        }
        
        return _instance;
    }
}