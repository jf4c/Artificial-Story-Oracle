using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.Pause;

public sealed class PauseCampaignHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<PauseCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(PauseCampaignCommand command)
    {
        var campaign = await _campaignRepository.GetByIdAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        if (campaign.CreatorId != command.CurrentPlayerId && campaign.GameMasterId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o criador ou mestre podem pausar a campanha.");

        campaign.Pause();
        await _unitOfWork.SaveChangesAsync();
    }
}

