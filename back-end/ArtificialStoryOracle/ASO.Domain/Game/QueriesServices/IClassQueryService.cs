using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;

namespace ASO.Domain.Game.QueriesServices;

public interface IClassQueryService : IQueryService<Class>
{
    Task<Class> GetById(Guid id);
    Task<List<Class>> GetByIds(List<Guid> ids);
}