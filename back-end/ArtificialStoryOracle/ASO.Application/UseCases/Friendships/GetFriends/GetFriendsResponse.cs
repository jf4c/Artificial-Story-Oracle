using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Friendships.GetReceivedRequests;

namespace ASO.Application.UseCases.Friendships.GetFriends;

public sealed record GetFriendsResponse : IResponse
{
    public required Guid FriendshipId { get; init; }
    public required PlayerBasicInfo Friend { get; init; }
    public required DateTime BecameFriendsAt { get; init; }
}

