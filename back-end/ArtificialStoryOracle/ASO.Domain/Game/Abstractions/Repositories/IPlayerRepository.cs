﻿using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Game.Abstractions.Repositories;

public interface IPlayerRepository : IRepository<Player>
{
    Task<Player> CreateAsync(Player player, CancellationToken cancellationToken = default);
    Task<Player?> GetByKeycloakUserIdAsync(Guid keycloakUserId, CancellationToken cancellationToken = default);
    Task<Player?> GetByIdAsync(Guid id);
    Task<List<Player>> GetAllAsync();
}

