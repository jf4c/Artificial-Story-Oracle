using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Campaigns.GetById;

public sealed class GetCampaignByIdHandler(ICampaignRepository campaignRepository)
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;

    public async Task<GetCampaignByIdResponse> HandleAsync(GetCampaignByIdQuery query)
    {
        var campaign = await _campaignRepository.GetByIdWithParticipantsAsync(query.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        return campaign.ToGetCampaignByIdResponse(query.CurrentPlayerId);
    }
}
