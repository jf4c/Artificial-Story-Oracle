using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Friendships.GetReceivedRequests;

public sealed record GetReceivedRequestsResponse : IResponse
{
    public required Guid Id { get; init; }
    public required PlayerBasicInfo Requester { get; init; }
    public required FriendshipStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

public sealed record PlayerBasicInfo
{
    public required Guid Id { get; init; }
    public required string NickName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
}

