using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Enums;
using Microsoft.EntityFrameworkCore;

namespace ASO.Application.UseCases.Friendships.SearchPlayers;

public sealed class SearchPlayersHandler(
    IPlayerRepository playerRepository,
    IFriendshipRepository friendshipRepository)
{
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly IFriendshipRepository _friendshipRepository = friendshipRepository;

    public async Task<List<SearchPlayersResponse>> HandleAsync(SearchPlayersQuery query, Guid currentPlayerId)
    {
        var searchTerm = query.SearchTerm.ToLower();
        
        var players = await _playerRepository.GetAllAsync();
        
        var filteredPlayers = players
            .Where(p => p.Id != currentPlayerId &&
                       (p.NickName.Nick.ToLower().Contains(searchTerm) ||
                        p.Name.FirstName.ToLower().Contains(searchTerm) ||
                        p.Name.LastName.ToLower().Contains(searchTerm)))
            .Take(20)
            .ToList();

        var result = new List<SearchPlayersResponse>();

        foreach (var player in filteredPlayers)
        {
            var friendship = await _friendshipRepository.GetExistingFriendshipAsync(currentPlayerId, player.Id);
            
            string? friendshipStatus = null;
            if (friendship != null)
            {
                friendshipStatus = friendship.Status switch
                {
                    FriendshipStatus.Accepted => "Amigos",
                    FriendshipStatus.Pending when friendship.RequesterId == currentPlayerId => "Convite enviado",
                    FriendshipStatus.Pending => "Convite recebido",
                    _ => null
                };
            }

            result.Add(new SearchPlayersResponse
            {
                Id = player.Id,
                NickName = player.NickName.Nick,
                FirstName = player.Name.FirstName,
                LastName = player.Name.LastName,
                FriendshipStatus = friendshipStatus
            });
        }

        return result;
    }
}

