using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Friendships.AcceptRequest;

public sealed record AcceptFriendRequestResponse : IResponse
{
    public required Guid Id { get; init; }
    public required FriendshipStatus Status { get; init; }
    public required DateTime AcceptedAt { get; init; }
}

