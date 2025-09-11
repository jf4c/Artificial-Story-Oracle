using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.QueriesServices.Abstractions;

namespace ASO.Domain.Game.Abstractions.QueriesServices;

public interface IClassQueryService : IQueryService<Class>
{
    Task<Class> GetById(Guid id);
    Task<List<Class>> GetByIds(List<Guid> ids);
    Task<List<Class>> GetAll();
}