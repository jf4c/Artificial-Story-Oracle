﻿using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public sealed class CampaignParticipantRepository(AppDbContext context) : ICampaignParticipantRepository
{
    private readonly AppDbContext _context = context;

    public async Task<CampaignParticipant?> GetByIdAsync(Guid id)
    {
        return await _context.CampaignParticipants
            .Include(cp => cp.Campaign)
            .Include(cp => cp.Player)
            .Include(cp => cp.Character)
            .FirstOrDefaultAsync(cp => cp.Id == id);
    }

    public async Task<CampaignParticipant?> GetByCampaignAndPlayerAsync(Guid campaignId, Guid playerId)
    {
        return await _context.CampaignParticipants
            .FirstOrDefaultAsync(cp => cp.CampaignId == campaignId && cp.PlayerId == playerId && cp.IsActive);
    }

    public async Task<List<CampaignParticipant>> GetByCampaignIdAsync(Guid campaignId)
    {
        return await _context.CampaignParticipants
            .Include(cp => cp.Player)
            .Include(cp => cp.Character)
            .Where(cp => cp.CampaignId == campaignId && cp.IsActive)
            .ToListAsync();
    }

    public async Task<CampaignParticipant> CreateAsync(CampaignParticipant participant)
    {
        await _context.CampaignParticipants.AddAsync(participant);
        return participant;
    }

    public Task DeleteAsync(CampaignParticipant participant)
    {
        _context.CampaignParticipants.Remove(participant);
        return Task.CompletedTask;
    }
}

