using System.Security.Claims;
using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.UseCases.Characters.GetAll;
using ASO.Domain.Game.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(
    ICreateCharacterHandler createCharacterHandler,
    IGetAllCharactersHandler getAllCharactersHandler,
    IPlayerRepository playerRepository,
    ICharacterRepository characterRepository
    ) : ControllerBase
{
    private readonly ICreateCharacterHandler _createCharacterHandler = createCharacterHandler;
    private readonly IGetAllCharactersHandler _getAllCharactersHandler = getAllCharactersHandler;
    private readonly IPlayerRepository _playerRepository = playerRepository;
    private readonly ICharacterRepository _characterRepository = characterRepository;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterInput input)
    {
        // Extrair Keycloak user ID do JWT
        var keycloakUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value
                          ?? throw new UnauthorizedAccessException("Token JWT inválido ou ausente.");

        if (!Guid.TryParse(keycloakUserId, out var userId))
        {
            return BadRequest(new { message = "User ID inválido no token." });
        }

        // Buscar Player pelo Keycloak user ID
        var player = await _playerRepository.GetByKeycloakUserIdAsync(userId);
        if (player == null)
        {
            return NotFound(new { message = "Player não encontrado. Crie um player primeiro." });
        }

        var command = input.ToCommand();
        // Substituir o PlayerId do input pelo PlayerId do player autenticado
        command = command with { PlayerId = player.Id };

        var result = await _createCharacterHandler.HandleAsync(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCharacters([FromQuery] GetAllCharacterQuery query)
    {
        var filter = query.ToFilter();
        
        // Se não veio PlayerId na query e usuário está autenticado, extrair do JWT
        if (!filter.PlayerId.HasValue && User.Identity?.IsAuthenticated == true)
        {
            var keycloakUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;
            
            if (!string.IsNullOrEmpty(keycloakUserId) && Guid.TryParse(keycloakUserId, out var userId))
            {
                var player = await _playerRepository.GetByKeycloakUserIdAsync(userId);
                if (player != null)
                {
                    filter = filter with { PlayerId = player.Id };
                }
            }
        }
        
        var response = await _getAllCharactersHandler.Handle(filter);
        return Ok(response);
    }

    [HttpGet("player/{playerId}")]
    public async Task<IActionResult> GetCharactersByPlayerId(Guid playerId)
    {
        var characters = await _characterRepository.GetByPlayerIdAsync(playerId);
        
        // Mapear para o response simplificado
        var response = characters.Select(c => new
        {
            c.Id,
            c.Name,
            Image = c.Image?.Url,
            Ancestry = c.Ancestry.Name,
            Class = c.Classes?.FirstOrDefault()?.Name ?? "Sem classe",
            c.Level
        }).ToList();
        
        return Ok(response);
    }
}