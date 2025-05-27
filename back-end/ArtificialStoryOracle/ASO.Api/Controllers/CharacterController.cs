using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Characters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(ICreateCharacterHandler createCharacterHandler) : ControllerBase
{
    private readonly ICreateCharacterHandler _createCharacterHandler = createCharacterHandler;

    [HttpPost]
    // [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterInput input)
    {
        var command = input.ToCommand();
        
        var player = await _createCharacterHandler.HandleAsync(command);
        
        return Ok(player);
    }
}