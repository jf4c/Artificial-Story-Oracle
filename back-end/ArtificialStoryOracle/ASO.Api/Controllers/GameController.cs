using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController(ICreateGameHandler createGameHandler) : ControllerBase
{
    private readonly ICreateGameHandler _createGameHandler = createGameHandler;
    
    [HttpPost]
    [Authorize]
    public IActionResult CreateGame([FromBody] CreateGameInput input)
    {
        var command = input.ToCommand();

        var player = _createGameHandler.Handle(command);

        return Ok(player);
    }
    
    // [HttpGet]
    // [Authorize]
    // public IActionResult GetGame(Guid input)
    // {
    //     var query = new GetPlayerByUserIdQuery(input);
    //
    //     var player = _createGameHandler.Handle(query);
    //
    //     return Ok(player);
    // }
}