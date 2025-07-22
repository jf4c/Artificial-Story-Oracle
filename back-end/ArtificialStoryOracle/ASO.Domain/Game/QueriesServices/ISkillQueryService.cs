using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.QueriesServices.Abstractions;

namespace ASO.Domain.Game.QueriesServices;

public interface ISkillQueryService : IQueryService<Skill>
{
    Task<List<Skill>> GetAll();
    Task<Skill> GetById(Guid id);
    Task<List<Skill>> GetByIds(List<Guid> ids);
}