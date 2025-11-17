using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public sealed class CampaignRepository(AppDbContext context) : ICampaignRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Campaign?> GetByIdAsync(Guid id)
    {
        return await _context.Campaigns
            .Include(c => c.Creator)
            .Include(c => c.GameMaster)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Campaign?> GetByIdWithParticipantsAsync(Guid id)
    {
        return await _context.Campaigns
            .Include(c => c.Creator)
            .Include(c => c.GameMaster)
            .Include(c => c.Participants.Where(p => p.IsActive))
                .ThenInclude(p => p.Player)
            .Include(c => c.Participants.Where(p => p.IsActive))
                .ThenInclude(p => p.Character)
                    .ThenInclude(ch => ch!.Ancestry)
            .Include(c => c.Participants.Where(p => p.IsActive))
                .ThenInclude(p => p.Character)
                    .ThenInclude(ch => ch!.Classes)
            .Include(c => c.Participants.Where(p => p.IsActive))
                .ThenInclude(p => p.Character)
                    .ThenInclude(ch => ch!.Image)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Campaign>> GetByPlayerIdAsync(Guid playerId, CampaignStatus? status = null)
    {
        var query = _context.Campaigns
            .Include(c => c.Creator)
            .Include(c => c.GameMaster)
            .Include(c => c.Participants.Where(p => p.IsActive))
            .Where(c => c.CreatorId == playerId || 
                       c.GameMasterId == playerId || 
                       c.Participants.Any(p => p.PlayerId == playerId && p.IsActive));

        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }

        return await query
            .OrderByDescending(c => c.Tracker.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<Campaign> CreateAsync(Campaign campaign)
    {
        await _context.Campaigns.AddAsync(campaign);
        return campaign;
    }

    public Task DeleteAsync(Campaign campaign)
    {
        _context.Campaigns.Remove(campaign);
        return Task.CompletedTask;
    }
}

