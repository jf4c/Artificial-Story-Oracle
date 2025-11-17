using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASO.Application.UseCases.Campaigns.GetById;

public sealed class GetCampaignByIdHandler(
    ICampaignRepository campaignRepository,
    ICharacterRepository characterRepository)
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly ICharacterRepository _characterRepository = characterRepository;

    public async Task<GetCampaignByIdResponse> HandleAsync(GetCampaignByIdQuery query)
    {
        var campaign = await _campaignRepository.GetByIdWithParticipantsAsync(query.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        var playerIds = campaign.Participants.Where(p => p.IsActive).Select(p => p.PlayerId).Distinct().ToList();
        var allCharacters = await _characterRepository.GetAll()
            .Where(c => playerIds.Contains(c.PlayerId))
            .ToListAsync();

        return campaign.ToGetCampaignByIdResponse(query.CurrentPlayerId, allCharacters);
    }
}
