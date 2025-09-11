using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.QueriesServices.Abstractions;

namespace ASO.Domain.Game.Abstractions.QueriesServices;

public interface IAncestryQueryService : IQueryService<Ancestry>
{
    Task<IEnumerable<Ancestry>> GetAll();
    Task<Ancestry> GetById(Guid id);
}