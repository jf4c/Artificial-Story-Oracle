using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Campaigns.GetMyCampaigns;

public sealed class GetMyCampaignsHandler(ICampaignRepository campaignRepository)
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;

    public async Task<List<CampaignListItem>> HandleAsync(GetMyCampaignsQuery query)
    {
        var campaigns = await _campaignRepository.GetByPlayerIdAsync(query.CurrentPlayerId, query.Status);
        return campaigns.Select(c => c.ToCampaignListItem(query.CurrentPlayerId)).ToList();
    }
}
