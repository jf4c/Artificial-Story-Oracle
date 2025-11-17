using ASO.Domain.Game.Dtos;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Game.Abstractions.Repositories;

public interface ICampaignRepository : IRepository<Campaign>
{
    Task<Campaign?> GetByIdAsync(Guid id);
    Task<Campaign?> GetByIdWithParticipantsAsync(Guid id);
    Task<List<Campaign>> GetByPlayerIdAsync(Guid playerId, CampaignStatus? status = null);
    Task<Campaign> CreateAsync(Campaign campaign);
    Task DeleteAsync(Campaign campaign);
}

