﻿using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public sealed class Friendship : Entity, IAggragateRoot
{
    private Friendship()
    {
        Requester = null!;
        Addressee = null!;
    }

    private Friendship(Guid requesterId, Guid addresseeId)
    {
        RequesterId = requesterId;
        AddresseeId = addresseeId;
        Status = FriendshipStatus.Pending;
    }

    public static Friendship Create(Guid requesterId, Guid addresseeId)
    {
        if (requesterId == addresseeId)
            throw new InvalidOperationException("Não é possível enviar convite de amizade para si mesmo.");

        return new Friendship(requesterId, addresseeId);
    }

    public void Accept()
    {
        if (Status != FriendshipStatus.Pending)
            throw new InvalidOperationException("Apenas convites pendentes podem ser aceitos.");

        Status = FriendshipStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        if (Status != FriendshipStatus.Pending)
            throw new InvalidOperationException("Apenas convites pendentes podem ser recusados.");

        Status = FriendshipStatus.Rejected;
        RejectedAt = DateTime.UtcNow;
    }

    public Guid RequesterId { get; private set; }
    public Guid AddresseeId { get; private set; }
    public FriendshipStatus Status { get; private set; }
    public DateTime? AcceptedAt { get; private set; }
    public DateTime? RejectedAt { get; private set; }

    public Player Requester { get; private set; }
    public Player Addressee { get; private set; }
}

