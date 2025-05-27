using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Game.Repositories.Abstractions;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character> Create(Character character);
}