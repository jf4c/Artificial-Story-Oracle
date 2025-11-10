using ASO.Application.UseCases.Campaigns.Create;
using ASO.Application.UseCases.Campaigns.GetById;
using ASO.Application.UseCases.Campaigns.GetMyCampaigns;
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

    public static GetCampaignByIdResponse ToGetCampaignByIdResponse(this Campaign campaign, Guid currentPlayerId)
    {
        return new GetCampaignByIdResponse
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            Creator = new PlayerBasicInfo
            {
                Id = campaign.Creator.Id,
                NickName = campaign.Creator.NickName.Nick,
                FirstName = campaign.Creator.Name.FirstName,
                LastName = campaign.Creator.Name.LastName
            },
            GameMaster = campaign.GameMaster != null ? new PlayerBasicInfo
            {
                Id = campaign.GameMaster.Id,
                NickName = campaign.GameMaster.NickName.Nick,
                FirstName = campaign.GameMaster.Name.FirstName,
                LastName = campaign.GameMaster.Name.LastName
            } : null,
            Status = campaign.Status,
            CreatedAt = campaign.Tracker.CreatedAtUtc,
            StartedAt = campaign.StartedAt,
            EndedAt = campaign.EndedAt,
            MaxPlayers = campaign.MaxPlayers,
            IsPublic = campaign.IsPublic,
            Participants = campaign.Participants.Where(p => p.IsActive).Select(p => p.ToParticipantWithDetails()).ToList(),
            CanEdit = campaign.CreatorId == currentPlayerId || campaign.GameMasterId == currentPlayerId,
            CanManageParticipants = campaign.CreatorId == currentPlayerId || campaign.GameMasterId == currentPlayerId
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

    public static ParticipantWithDetails ToParticipantWithDetails(this CampaignParticipant participant)
    {
        return new ParticipantWithDetails
        {
            Id = participant.Id,
            Player = new PlayerBasicInfo
            {
                Id = participant.Player.Id,
                NickName = participant.Player.NickName.Nick,
                FirstName = participant.Player.Name.FirstName,
                LastName = participant.Player.Name.LastName
            },
            Character = participant.Character != null ? new CharacterBasicInfo
            {
                Id = participant.Character.Id,
                Name = participant.Character.Name,
                Race = participant.Character.Ancestry.Name,
                Class = participant.Character.Classes?.FirstOrDefault()?.Name ?? "Sem classe",
                Level = participant.Character.Level
            } : null,
            Role = participant.Role,
            JoinedAt = participant.JoinedAt
        };
    }
}

