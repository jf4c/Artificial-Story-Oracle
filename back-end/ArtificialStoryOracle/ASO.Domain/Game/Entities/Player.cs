﻿﻿﻿using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using Email = ASO.Domain.Shared.ValueObjects.Email;

namespace ASO.Domain.Game.Entities;

public class Player : Entity, IAggragateRoot
{
    #region Constructors
    private Player()
    {
        Name = null!;
        Email = null!;
        NickName = null!;
        TypePlayer = TypePlayer.Player;
    }

    private Player(Guid keycloakUserId, string firstName, string lastName, string address, string nickName)
    {
        KeycloakUserId = keycloakUserId;
        Name = Name.Create(firstName, lastName);
        Email = Email.Create(address);
        NickName = Nickname.Create(nickName);
        TypePlayer = TypePlayer.Player;
    }
    #endregion

    #region Factory Methods
    public static Player Create(Guid keycloakUserId, string firstName, string lastName, string address, string nick)
        => new(keycloakUserId, firstName, lastName, address, nick);
    #endregion

    #region Proporties
    public Guid KeycloakUserId { get; }
    public Name Name { get; }
    public Email Email { get; }
    public Nickname NickName { get; }
    public TypePlayer TypePlayer { get; }
    
    public ICollection<Friendship> SentFriendRequests { get; } = new List<Friendship>();
    public ICollection<Friendship> ReceivedFriendRequests { get; } = new List<Friendship>();
    public ICollection<Campaign> CreatedCampaigns { get; } = new List<Campaign>();
    public ICollection<Campaign> MasteredCampaigns { get; } = new List<Campaign>();
    public ICollection<CampaignParticipant> CampaignParticipations { get; } = new List<CampaignParticipant>();
    #endregion
}