using ASO.Application.UseCases.Friendships.AcceptRequest;
using ASO.Application.UseCases.Friendships.GetReceivedRequests;
using ASO.Application.UseCases.Friendships.GetSentRequests;
using ASO.Application.UseCases.Friendships.SendRequest;
using ASO.Domain.Game.Entities;

namespace ASO.Application.Mappers;

public static class FriendshipMapper
{
    public static SendFriendRequestResponse ToSendFriendRequestResponse(this Friendship friendship)
    {
        return new SendFriendRequestResponse
        {
            Id = friendship.Id,
            RequesterId = friendship.RequesterId,
            AddresseeId = friendship.AddresseeId,
            Status = friendship.Status,
            CreatedAt = friendship.Tracker.CreatedAtUtc
        };
    }

    public static AcceptFriendRequestResponse ToAcceptFriendRequestResponse(this Friendship friendship)
    {
        return new AcceptFriendRequestResponse
        {
            Id = friendship.Id,
            Status = friendship.Status,
            AcceptedAt = friendship.AcceptedAt!.Value
        };
    }

    public static GetReceivedRequestsResponse ToGetReceivedRequestsResponse(this Friendship friendship)
    {
        return new GetReceivedRequestsResponse
        {
            Id = friendship.Id,
            Requester = new PlayerBasicInfo
            {
                Id = friendship.Requester.Id,
                NickName = friendship.Requester.NickName.Nick,
                FirstName = friendship.Requester.Name.FirstName,
                LastName = friendship.Requester.Name.LastName
            },
            Status = friendship.Status,
            CreatedAt = friendship.Tracker.CreatedAtUtc
        };
    }

    public static GetSentRequestsResponse ToGetSentRequestsResponse(this Friendship friendship)
    {
        return new GetSentRequestsResponse
        {
            Id = friendship.Id,
            Addressee = new PlayerBasicInfo
            {
                Id = friendship.Addressee.Id,
                NickName = friendship.Addressee.NickName.Nick,
                FirstName = friendship.Addressee.Name.FirstName,
                LastName = friendship.Addressee.Name.LastName
            },
            Status = friendship.Status,
            CreatedAt = friendship.Tracker.CreatedAtUtc
        };
    }
}

