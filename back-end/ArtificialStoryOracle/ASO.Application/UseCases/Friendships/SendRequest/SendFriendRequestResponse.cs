using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Friendships.SendRequest;

public sealed record SendFriendRequestResponse : IResponse
{
    public required Guid Id { get; init; }
    public required Guid RequesterId { get; init; }
    public required Guid AddresseeId { get; init; }
    public required FriendshipStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

