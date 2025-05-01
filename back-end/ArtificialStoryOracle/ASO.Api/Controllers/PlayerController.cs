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

    [HttpGet]
    [Authorize]
    public IActionResult GetPlayer(Guid input)
    {
        var query = new GetPlayerByUserIdQuery(input);

        var player = getPlayerByUserIdHandler.Handle(query);

        return Ok(player);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreatePlayer(string nickName)
    {
        var userId = User.FindFirst("sub")!.Value;
        var email = User.FindFirst("email")!.Value;
        var name = User.FindFirst("name")!.Value;

        var command = new CreatePlayerCommand(name, email, nickName, userId);

        var player = _createPlayerHandler.Handle(command);

        return Ok(player);
    }
}