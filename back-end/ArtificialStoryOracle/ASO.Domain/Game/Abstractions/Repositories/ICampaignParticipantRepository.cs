using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.Game.Abstractions.Repositories;

public interface ICampaignParticipantRepository : IRepository<CampaignParticipant>
{
    Task<CampaignParticipant?> GetByIdAsync(Guid id);
    Task<CampaignParticipant?> GetByCampaignAndPlayerAsync(Guid campaignId, Guid playerId);
    Task<List<CampaignParticipant>> GetByCampaignIdAsync(Guid campaignId);
    Task<CampaignParticipant> CreateAsync(CampaignParticipant participant);
    Task DeleteAsync(CampaignParticipant participant);
}

