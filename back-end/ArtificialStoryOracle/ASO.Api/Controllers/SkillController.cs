using ASO.Application.Abstractions.UseCase.Ancestry;
using ASO.Application.Abstractions.UseCase.Skills;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController(IGetAllSkillsHandler getAllSkillHandler) : ControllerBase
{
    private readonly IGetAllSkillsHandler _getAllSkillHandler = getAllSkillHandler;

    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetAllSkill()
    {
        var skills = await _getAllSkillHandler.Handle();
        
        return Ok(skills);
    }
}