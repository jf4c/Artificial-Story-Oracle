using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Friendships.GetSentRequests;

public sealed class GetSentRequestsHandler(IFriendshipRepository friendshipRepository)
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    public async Task<List<GetSentRequestsResponse>> HandleAsync(Guid currentPlayerId)
    {
        var friendships = await _friendshipRepository.GetSentRequestsAsync(currentPlayerId);
        return friendships.Select(f => f.ToGetSentRequestsResponse()).ToList();
    }
}

