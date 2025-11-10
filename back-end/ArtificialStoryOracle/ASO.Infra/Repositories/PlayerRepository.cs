﻿using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public sealed class PlayerRepository(AppDbContext context) : IPlayerRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Player> CreateAsync(Player player, CancellationToken cancellationToken = default)
    {
        await _context.Players.AddAsync(player, cancellationToken);
        return player;
    }

    public async Task<Player?> GetByKeycloakUserIdAsync(Guid keycloakUserId, CancellationToken cancellationToken = default)
    {
        return await _context.Players
            .FirstOrDefaultAsync(p => p.KeycloakUserId == keycloakUserId, cancellationToken);
    }

    public async Task<Player?> GetByIdAsync(Guid id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<List<Player>> GetAllAsync()
    {
        return await _context.Players.ToListAsync();
    }
}

