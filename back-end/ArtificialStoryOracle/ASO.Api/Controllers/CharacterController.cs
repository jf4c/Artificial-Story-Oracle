using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.UseCases.Characters.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController(
    ICreateCharacterHandler createCharacterHandler,
    IGetAllCharactersHandler getAllCharactersHandler
    ) : ControllerBase
{
    private readonly ICreateCharacterHandler _createCharacterHandler = createCharacterHandler;
    private readonly IGetAllCharactersHandler _getAllCharactersHandler = getAllCharactersHandler;

    [HttpPost]
    // [Authorize]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterInput input)
    {
        var command = input.ToCommand();

        var player = await _createCharacterHandler.HandleAsync(command);

        return Ok(player);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCharacters([FromQuery] GetAllCharacterQuery query)
    {
        var filter = query.ToFilter();
        var response = await _getAllCharactersHandler.Handle(filter);
        return Ok(response);
    }
}