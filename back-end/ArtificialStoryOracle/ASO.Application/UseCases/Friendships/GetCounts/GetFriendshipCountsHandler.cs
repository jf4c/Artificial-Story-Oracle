using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Friendships.GetCounts;

public sealed class GetFriendshipCountsHandler(IFriendshipRepository friendshipRepository)
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    public async Task<GetFriendshipCountsResponse> HandleAsync(Guid currentPlayerId)
    {
        var friends = await _friendshipRepository.GetFriendsAsync(currentPlayerId);
        var receivedRequests = await _friendshipRepository.GetReceivedRequestsAsync(currentPlayerId);
        var sentRequests = await _friendshipRepository.GetSentRequestsAsync(currentPlayerId);

        return new GetFriendshipCountsResponse
        {
            TotalFriends = friends.Count,
            PendingReceived = receivedRequests.Count,
            PendingSent = sentRequests.Count
        };
    }
}

