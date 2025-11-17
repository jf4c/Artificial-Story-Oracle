using ASO.Application.Abstractions.Shared;
using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;
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
            command.IsPublic,
            command.StoryIntroduction);

        // Adicionar criador como GameMaster primeiro
        var creatorParticipant = command.Participants.FirstOrDefault(p => p.PlayerId == command.CurrentPlayerId);
        if (creatorParticipant != null)
        {
            var gmParticipant = CampaignParticipant.Create(campaign.Id, command.CurrentPlayerId, ParticipantRole.GameMaster);
            gmParticipant.SetCharacter(creatorParticipant.CharacterId);
            campaign.Participants.Add(gmParticipant);
        }
        else
        {
            // Se o criador não estiver na lista de participantes, adiciona sem personagem
            var gmParticipant = CampaignParticipant.Create(campaign.Id, command.CurrentPlayerId, ParticipantRole.GameMaster);
            campaign.Participants.Add(gmParticipant);
        }

        // Associar demais participantes como Players
        foreach (var participant in command.Participants.Where(p => p.PlayerId != command.CurrentPlayerId))
        {
            var player = await _playerRepository.GetByIdAsync(participant.PlayerId)
                ?? throw new InvalidOperationException($"Player {participant.PlayerId} não encontrado.");
            
            var campaignParticipant = CampaignParticipant.Create(campaign.Id, participant.PlayerId, ParticipantRole.Player);
            campaignParticipant.SetCharacter(participant.CharacterId);
            campaign.Participants.Add(campaignParticipant);
        }

        await _campaignRepository.CreateAsync(campaign);
        await _unitOfWork.SaveChangesAsync();

        return campaign.ToCreateCampaignResponse();
    }
}
