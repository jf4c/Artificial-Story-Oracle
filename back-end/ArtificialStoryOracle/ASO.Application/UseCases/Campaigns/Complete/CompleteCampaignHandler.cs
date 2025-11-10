using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.Complete;

public sealed class CompleteCampaignHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<CompleteCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(CompleteCampaignCommand command)
    {
        var campaign = await _campaignRepository.GetByIdAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        if (campaign.CreatorId != command.CurrentPlayerId && campaign.GameMasterId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o criador ou mestre podem finalizar a campanha.");

        campaign.Complete();
        await _unitOfWork.SaveChangesAsync();
    }
}
