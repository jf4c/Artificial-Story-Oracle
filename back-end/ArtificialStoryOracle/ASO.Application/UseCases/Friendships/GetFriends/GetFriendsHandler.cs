using ASO.Application.UseCases.Friendships.GetReceivedRequests;
using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Friendships.GetFriends;

public sealed class GetFriendsHandler(IFriendshipRepository friendshipRepository)
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    public async Task<List<GetFriendsResponse>> HandleAsync(Guid currentPlayerId)
    {
        var friendships = await _friendshipRepository.GetFriendsAsync(currentPlayerId);
        
        return friendships.Select(f =>
        {
            var friend = f.RequesterId == currentPlayerId ? f.Addressee : f.Requester;
            
            return new GetFriendsResponse
            {
                FriendshipId = f.Id,
                Friend = new PlayerBasicInfo
                {
                    Id = friend.Id,
                    NickName = friend.NickName.Nick,
                    FirstName = friend.Name.FirstName,
                    LastName = friend.Name.LastName
                },
                BecameFriendsAt = f.AcceptedAt!.Value
            };
        }).ToList();
    }
}

