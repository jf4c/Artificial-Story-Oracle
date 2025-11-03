using ASO.Application.Abstractions.UseCase.Ancestry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AncestryController(IGetAllAncestryHandler getAllAncestryHandler) : ControllerBase
{
    private readonly IGetAllAncestryHandler _getAllAncestryHandler = getAllAncestryHandler;

    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetAllAncestries()
    {
        var ancestries = await _getAllAncestryHandler.Handle();
        return Ok(ancestries);
    }
}