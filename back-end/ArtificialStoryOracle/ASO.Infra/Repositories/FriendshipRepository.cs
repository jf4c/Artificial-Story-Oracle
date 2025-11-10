using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public sealed class FriendshipRepository(AppDbContext context) : IFriendshipRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Friendship?> GetByIdAsync(Guid id)
    {
        return await _context.Friendships
            .Include(f => f.Requester)
            .Include(f => f.Addressee)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Friendship?> GetExistingFriendshipAsync(Guid requesterId, Guid addresseeId)
    {
        return await _context.Friendships
            .FirstOrDefaultAsync(f => 
                (f.RequesterId == requesterId && f.AddresseeId == addresseeId) ||
                (f.RequesterId == addresseeId && f.AddresseeId == requesterId));
    }

    public async Task<List<Friendship>> GetReceivedRequestsAsync(Guid playerId)
    {
        return await _context.Friendships
            .Include(f => f.Requester)
            .Where(f => f.AddresseeId == playerId && f.Status == FriendshipStatus.Pending)
            .OrderByDescending(f => f.Tracker.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<List<Friendship>> GetSentRequestsAsync(Guid playerId)
    {
        return await _context.Friendships
            .Include(f => f.Addressee)
            .Where(f => f.RequesterId == playerId && f.Status == FriendshipStatus.Pending)
            .OrderByDescending(f => f.Tracker.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<List<Friendship>> GetFriendsAsync(Guid playerId)
    {
        return await _context.Friendships
            .Include(f => f.Requester)
            .Include(f => f.Addressee)
            .Where(f => 
                (f.RequesterId == playerId || f.AddresseeId == playerId) && 
                f.Status == FriendshipStatus.Accepted)
            .OrderByDescending(f => f.AcceptedAt)
            .ToListAsync();
    }

    public async Task<Friendship> CreateAsync(Friendship friendship)
    {
        await _context.Friendships.AddAsync(friendship);
        return friendship;
    }

    public Task DeleteAsync(Friendship friendship)
    {
        _context.Friendships.Remove(friendship);
        return Task.CompletedTask;
    }
}

