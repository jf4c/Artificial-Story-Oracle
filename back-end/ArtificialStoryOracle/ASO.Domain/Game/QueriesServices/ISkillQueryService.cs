using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;

namespace ASO.Domain.Game.QueriesServices;

public interface ISkillQueryService : IQueryService<Skill>
{
    Task<List<Skill>> GetAll();
    Task<Skill> GetById(Guid id);
    Task<List<Skill>> GetByIds(List<Guid> ids);
}