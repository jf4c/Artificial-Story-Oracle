﻿using ASO.Domain.Identity.Entities;
using ASO.Domain.Identity.Repositories.Abstractions;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public sealed class PlayerUserRepository(AppDbContext context) : IPlayerUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<PlayerUser?> GetByUserId(Guid userId)
    {
        var player = await _context.Set<PlayerUser>()
            .FirstOrDefaultAsync(p => p.KeycloakUserId == userId);
        
        return player;
    }

    public async Task<PlayerUser> Create(PlayerUser player)
    {
        await _context.Set<PlayerUser>().AddAsync(player);
        return player;
    }
}

