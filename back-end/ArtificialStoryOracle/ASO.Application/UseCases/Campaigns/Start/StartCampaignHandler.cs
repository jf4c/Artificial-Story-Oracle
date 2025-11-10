using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.Start;

public sealed class StartCampaignHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<StartCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(StartCampaignCommand command)
    {
        var campaign = await _campaignRepository.GetByIdWithParticipantsAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        if (campaign.CreatorId != command.CurrentPlayerId && campaign.GameMasterId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o criador ou mestre podem iniciar a campanha.");

        campaign.Start();
        await _unitOfWork.SaveChangesAsync();
    }
}

