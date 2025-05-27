using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;

namespace ASO.Domain.Game.QueriesServices;

public interface IAncestryQueryService : IQueryService<Ancestry>
{
    Task<IEnumerable<Ancestry>> GetAll();
    Task<Ancestry> GetById(Guid id);
}