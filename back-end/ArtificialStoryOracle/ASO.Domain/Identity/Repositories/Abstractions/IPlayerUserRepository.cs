﻿using ASO.Domain.Game.Entities;
using ASO.Domain.Identity.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Identity.Repositories.Abstractions;

public interface IPlayerUserRepository : IRepository<PlayerUser>
{
    Task<PlayerUser?> GetByUserId(Guid userId);
    Task<PlayerUser> Create(PlayerUser player);
}