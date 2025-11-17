using ASO.Application.UseCases.Campaigns.Create;
using ASO.Application.UseCases.Campaigns.GetById;
using ASO.Application.UseCases.Campaigns.GetMyCampaigns;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;

namespace ASO.Application.Mappers;

public static class CampaignMapper
{
    public static CreateCampaignResponse ToCreateCampaignResponse(this Campaign campaign)
    {
        return new CreateCampaignResponse
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            CreatorId = campaign.CreatorId,
            GameMasterId = campaign.GameMasterId,
            Status = campaign.Status,
            CreatedAt = campaign.Tracker.CreatedAtUtc,
            MaxPlayers = campaign.MaxPlayers,
            IsPublic = campaign.IsPublic,
            ParticipantsCount = campaign.Participants.Count(p => p.IsActive)
        };
    }

    public static GetCampaignByIdResponse ToGetCampaignByIdResponse(this Campaign campaign, Guid currentPlayerId, List<Character> allCharacters)
    {
        var userRole = campaign.CreatorId == currentPlayerId ? "creator" :
                       campaign.GameMasterId == currentPlayerId ? "gameMaster" : "player";

        return new GetCampaignByIdResponse
        {
            Id = campaign.Id,
            CreatorId = campaign.CreatorId,
            Name = campaign.Name,
            Description = campaign.Description,
            Image = null, // TODO: Adicionar campo Image na entidade Campaign
            UserRole = userRole,
            Status = campaign.Status,
            CreatedAt = campaign.Tracker.CreatedAtUtc,
            MaxPlayers = campaign.MaxPlayers,
            IsPublic = campaign.IsPublic,
            StoryIntroduction = campaign.StoryIntroduction,
            Settings = new CampaignSettings
            {
                System = "D&D 5e", // TODO: Adicionar campo System na entidade Campaign
                AllowCharacterCreation = true // TODO: Adicionar campo AllowCharacterCreation
            },
            Participants = campaign.Participants
                .Where(p => p.IsActive)
                .Select(p => p.ToParticipantWithDetails(allCharacters, campaign.Id))
                .ToList(),
            CanEdit = campaign.CreatorId == currentPlayerId || campaign.GameMasterId == currentPlayerId,
            CanManageParticipants = campaign.CreatorId == currentPlayerId || campaign.GameMasterId == currentPlayerId,
            Sessions = new List<SessionInfo>(), // Mockado - implementar quando houver feature de sessões
            Statistics = null, // Mockado - implementar quando houver feature de estatísticas
            World = null // Mockado - implementar quando houver feature de mundo
        };
    }

    public static CampaignListItem ToCampaignListItem(this Campaign campaign, Guid currentPlayerId)
    {
        var myRole = campaign.CreatorId == currentPlayerId ? "creator" :
                     campaign.GameMasterId == currentPlayerId ? "gameMaster" : "player";

        return new CampaignListItem
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            Status = campaign.Status,
            CreatedAt = campaign.Tracker.CreatedAtUtc,
            ParticipantsCount = campaign.Participants.Count(p => p.IsActive),
            MaxPlayers = campaign.MaxPlayers,
            MyRole = myRole,
            IsCreator = campaign.CreatorId == currentPlayerId
        };
    }

    public static ParticipantWithDetails ToParticipantWithDetails(this CampaignParticipant participant, List<Character> allCharacters, Guid campaignId)
    {
        // Buscar o character vinculado ao participante
        var character = allCharacters
            .Where(c => c.Id == participant.CharacterId)
            .Select(c => new CharacterBasicInfo
            {
                Id = c.Id,
                Name = c.Name,
                Class = c.Classes?.FirstOrDefault()?.Name ?? "Sem classe",
                Race = c.Ancestry.Name,
                Ancestry = c.Ancestry.Name,
                Level = c.Level,
                Description = c.Backstory,
                Image = c.Image?.Url,
                IsActive = true
            })
            .FirstOrDefault();

        return new ParticipantWithDetails
        {
            Id = participant.Id,
            UserId = participant.Player.Id,
            UserName = participant.Player.NickName.Nick,
            UserAvatar = null, // TODO: Adicionar avatar no Player
            Role = participant.Role,
            JoinedAt = participant.JoinedAt,
            Character = character
        };
    }
}

