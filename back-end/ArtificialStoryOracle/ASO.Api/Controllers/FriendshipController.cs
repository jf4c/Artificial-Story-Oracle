using System.Security.Claims;
using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.UseCases.Friendships.AcceptRequest;
using ASO.Application.UseCases.Friendships.GetCounts;
using ASO.Application.UseCases.Friendships.GetFriends;
using ASO.Application.UseCases.Friendships.GetReceivedRequests;
using ASO.Application.UseCases.Friendships.GetSentRequests;
using ASO.Application.UseCases.Friendships.Remove;
using ASO.Application.UseCases.Friendships.RejectRequest;
using ASO.Application.UseCases.Friendships.SearchPlayers;
using ASO.Application.UseCases.Friendships.SendRequest;
using ASO.Domain.Game.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class FriendshipController(
    SendFriendRequestHandler sendRequestHandler,
    AcceptFriendRequestHandler acceptRequestHandler,
    RejectFriendRequestHandler rejectRequestHandler,
    RemoveFriendshipHandler removeFriendshipHandler,
    GetReceivedRequestsHandler getReceivedRequestsHandler,
    GetSentRequestsHandler getSentRequestsHandler,
    GetFriendsHandler getFriendsHandler,
    SearchPlayersHandler searchPlayersHandler,
    GetFriendshipCountsHandler getCountsHandler,
    IPlayerRepository playerRepository) : ControllerBase
{
    private readonly SendFriendRequestHandler _sendRequestHandler = sendRequestHandler;
    private readonly AcceptFriendRequestHandler _acceptRequestHandler = acceptRequestHandler;
    private readonly RejectFriendRequestHandler _rejectRequestHandler = rejectRequestHandler;
    private readonly RemoveFriendshipHandler _removeFriendshipHandler = removeFriendshipHandler;
    private readonly GetReceivedRequestsHandler _getReceivedRequestsHandler = getReceivedRequestsHandler;
    private readonly GetSentRequestsHandler _getSentRequestsHandler = getSentRequestsHandler;
    private readonly GetFriendsHandler _getFriendsHandler = getFriendsHandler;
    private readonly SearchPlayersHandler _searchPlayersHandler = searchPlayersHandler;
    private readonly GetFriendshipCountsHandler _getCountsHandler = getCountsHandler;
    private readonly IPlayerRepository _playerRepository = playerRepository;

    private async Task<Guid> GetCurrentPlayerIdAsync()
    {
        var keycloakUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value
                          ?? throw new UnauthorizedAccessException("User ID não encontrado no token.");

        if (!Guid.TryParse(keycloakUserId, out var userId))
            throw new UnauthorizedAccessException("User ID inválido.");

        var player = await _playerRepository.GetByKeycloakUserIdAsync(userId)
            ?? throw new InvalidOperationException("Player não encontrado.");

        return player.Id;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendFriendRequest([FromBody] SendFriendRequestInput input)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new SendFriendRequestCommand(playerId, input.AddresseeId);
        var response = await _sendRequestHandler.HandleAsync(command);
        return Ok(response);
    }

    [HttpPost("{friendshipId}/accept")]
    public async Task<IActionResult> AcceptFriendRequest(Guid friendshipId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new AcceptFriendRequestCommand(playerId, friendshipId);
        var response = await _acceptRequestHandler.HandleAsync(command);
        return Ok(response);
    }

    [HttpPost("{friendshipId}/reject")]
    public async Task<IActionResult> RejectFriendRequest(Guid friendshipId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new RejectFriendRequestCommand(playerId, friendshipId);
        await _rejectRequestHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{friendshipId}")]
    public async Task<IActionResult> RemoveFriendship(Guid friendshipId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new RemoveFriendshipCommand(playerId, friendshipId);
        await _removeFriendshipHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpGet("requests/received")]
    public async Task<IActionResult> GetReceivedRequests()
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var response = await _getReceivedRequestsHandler.HandleAsync(playerId);
        return Ok(response);
    }

    [HttpGet("requests/sent")]
    public async Task<IActionResult> GetSentRequests()
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var response = await _getSentRequestsHandler.HandleAsync(playerId);
        return Ok(response);
    }

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriends()
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var response = await _getFriendsHandler.HandleAsync(playerId);
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPlayers([FromQuery] string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return BadRequest(new { message = "Termo de busca é obrigatório." });

        var playerId = await GetCurrentPlayerIdAsync();
        var query = new SearchPlayersQuery(searchTerm);
        var response = await _searchPlayersHandler.HandleAsync(query, playerId);
        return Ok(response);
    }

    [HttpGet("counts")]
    public async Task<IActionResult> GetCounts()
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var response = await _getCountsHandler.HandleAsync(playerId);
        return Ok(response);
    }
}

