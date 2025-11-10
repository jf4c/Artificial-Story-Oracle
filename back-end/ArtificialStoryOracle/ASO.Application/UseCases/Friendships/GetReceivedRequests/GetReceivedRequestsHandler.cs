using ASO.Application.Abstractions.Shared;
using ASO.Application.Mappers;
using ASO.Domain.Game.Abstractions.Repositories;

namespace ASO.Application.UseCases.Friendships.GetReceivedRequests;

public sealed class GetReceivedRequestsHandler(IFriendshipRepository friendshipRepository) 
{
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    public async Task<List<GetReceivedRequestsResponse>> HandleAsync(Guid currentPlayerId)
    {
        var friendships = await _friendshipRepository.GetReceivedRequestsAsync(currentPlayerId);
        return friendships.Select(f => f.ToGetReceivedRequestsResponse()).ToList();
    }
}

