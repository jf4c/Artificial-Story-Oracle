using ASO.Application.Abstractions.UseCase.Images;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController(IGetAllImagesHandler getAllImagesHandler) : ControllerBase
{
    private readonly IGetAllImagesHandler _getAllImagesHandler = getAllImagesHandler;

    [HttpGet]
    public async Task<IActionResult> GetAllImage()
    {
        var images = await _getAllImagesHandler.Handle();
        
        return Ok(images);
    }
}