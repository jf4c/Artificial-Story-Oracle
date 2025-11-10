using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public sealed class CampaignParticipant : Entity, IAggragateRoot
{
    private CampaignParticipant()
    {
        Campaign = null!;
        Player = null!;
    }

    private CampaignParticipant(Guid campaignId, Guid playerId, ParticipantRole role)
    {
        CampaignId = campaignId;
        PlayerId = playerId;
        Role = role;
        JoinedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public static CampaignParticipant Create(Guid campaignId, Guid playerId, ParticipantRole role)
    {
        return new CampaignParticipant(campaignId, playerId, role);
    }

    public void SetCharacter(Guid? characterId)
    {
        if (Role == ParticipantRole.GameMaster && characterId.HasValue)
            throw new InvalidOperationException("Game Master não precisa de personagem.");

        CharacterId = characterId;
    }

    public void ChangeRole(ParticipantRole newRole)
    {
        Role = newRole;

        if (newRole == ParticipantRole.GameMaster)
            CharacterId = null;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public Guid CampaignId { get; private set; }
    public Guid PlayerId { get; private set; }
    public Guid? CharacterId { get; private set; }
    public ParticipantRole Role { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public bool IsActive { get; private set; }

    public Campaign Campaign { get; private set; }
    public Player Player { get; private set; }
    public Character? Character { get; private set; }
}

