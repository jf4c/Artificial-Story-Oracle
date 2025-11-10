using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Game.Abstractions.Repositories;

public interface IFriendshipRepository : IRepository<Friendship>
{
    Task<Friendship?> GetByIdAsync(Guid id);
    Task<Friendship?> GetExistingFriendshipAsync(Guid requesterId, Guid addresseeId);
    Task<List<Friendship>> GetReceivedRequestsAsync(Guid playerId);
    Task<List<Friendship>> GetSentRequestsAsync(Guid playerId);
    Task<List<Friendship>> GetFriendsAsync(Guid playerId);
    Task<Friendship> CreateAsync(Friendship friendship);
    Task DeleteAsync(Friendship friendship);
}

