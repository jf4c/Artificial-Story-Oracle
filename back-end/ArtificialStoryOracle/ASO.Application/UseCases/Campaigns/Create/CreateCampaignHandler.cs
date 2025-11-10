using ASO.Application.Abstractions.Shared;
using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.Create;

public sealed class CreateCampaignHandler(
    ICampaignRepository campaignRepository,
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<CreateCampaignCommand, CreateCampaignResponse>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateCampaignResponse> HandleAsync(CreateCampaignCommand command)
    {
        var creator = await _playerRepository.GetByIdAsync(command.CurrentPlayerId)
            ?? throw new InvalidOperationException("Jogador não encontrado.");

        var campaign = Campaign.Create(
            command.CurrentPlayerId,
            command.Name,
            command.Description,
            command.MaxPlayers,
            command.IsPublic);

        await _campaignRepository.CreateAsync(campaign);
        await _unitOfWork.SaveChangesAsync();

        return campaign.ToCreateCampaignResponse();
    }
}
