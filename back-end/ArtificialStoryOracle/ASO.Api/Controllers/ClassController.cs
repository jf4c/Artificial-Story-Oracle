using ASO.Application.Abstractions.UseCase.Classes;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ClassController(IGetAllClassesHandler getAllClassesHandler) : ControllerBase
{
    private readonly IGetAllClassesHandler _getAllClassesHandler = getAllClassesHandler;
    
    [HttpGet]
    public async Task<IActionResult> GetAllClasses()
    {
        var classes = await _getAllClassesHandler.Handle();

        return Ok(classes);
    }
}