using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.Update;

public sealed class UpdateCampaignHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<UpdateCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(UpdateCampaignCommand command)
    {
        var campaign = await _campaignRepository.GetByIdAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        if (campaign.CreatorId != command.CurrentPlayerId && campaign.GameMasterId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o criador ou mestre podem editar a campanha.");

        campaign.Update(command.Name, command.Description, command.MaxPlayers, command.Status);
        await _unitOfWork.SaveChangesAsync();
    }
}

