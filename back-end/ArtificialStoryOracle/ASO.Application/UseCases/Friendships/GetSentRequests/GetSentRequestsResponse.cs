using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Friendships.GetReceivedRequests;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Friendships.GetSentRequests;

public sealed record GetSentRequestsResponse : IResponse
{
    public required Guid Id { get; init; }
    public required PlayerBasicInfo Addressee { get; init; }
    public required FriendshipStatus Status { get; init; }
    public required DateTime CreatedAt { get; init; }
}

