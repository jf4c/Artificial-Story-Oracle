using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;

namespace ASO.Domain.Game.QueriesServices;

public interface IExpertiseQueryService : IQueryService<Expertise>
{
    Task<Expertise> GetById(Guid id);
    Task<List<Expertise>> GetByIds(List<Guid> ids);
}