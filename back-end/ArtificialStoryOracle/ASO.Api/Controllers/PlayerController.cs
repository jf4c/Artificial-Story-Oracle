using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.UseCases.Players.Create;
using ASO.Application.UseCases.Players.GetByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController(
    ICreatePlayerHandler createPlayerHandler,
    IGetPlayerByUserIdHandler getPlayerByUserIdHandler) : ControllerBase
{
    private readonly ICreatePlayerHandler _createPlayerHandler = createPlayerHandler;
    private readonly IGetPlayerByUserIdHandler _getPlayerByUserIdHandler = getPlayerByUserIdHandler;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPlayer()
    {
        var keycloakUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value
                          ?? throw new ArgumentException("Claim de user id não encontrada no token.");

        if (!Guid.TryParse(keycloakUserId, out var userId))
        {
            return BadRequest(new { message = "User ID inválido." });
        }

        var query = new GetPlayerByUserIdQuery(userId);
        var player = await _getPlayerByUserIdHandler.Handle(query);

        if (player == null)
        {
            return NotFound(new { message = "Player não encontrado." });
        }

        return Ok(player);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePlayer()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value
                          ?? throw new ArgumentException("Claim de user id não encontrada no token.");

        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value 
                    ?? User.FindFirst("email")?.Value 
                    ?? string.Empty;
        
        var name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value 
                   ?? User.FindFirst("name")?.Value 
                   ?? string.Empty;
        
        var nick = User.FindFirst("preferred_username")?.Value ?? string.Empty;

        var command = new CreatePlayerCommand(name, email, nick, userIdClaim);

        var player = await _createPlayerHandler.HandleAsync(command);

        return Ok(player);
    }
}