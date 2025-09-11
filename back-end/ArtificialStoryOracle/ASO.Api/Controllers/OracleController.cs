using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Oracle;
using ASO.Application.UseCases.Oracle;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OracleController(
    IGenerateCharacterBackstory generateCharacterBackstory,
    IGenerateCharactersNames generateCharactersNames,
    IGenerateCampaignBackstory generateCampaignBackstory
    ) : ControllerBase
{
    private readonly IGenerateCharacterBackstory _generateCharacterBackstory = generateCharacterBackstory;
    private readonly IGenerateCharactersNames _generateCharacterNames = generateCharactersNames;
    private readonly IGenerateCampaignBackstory _generateCampaignBackstory = generateCampaignBackstory;
    
    [HttpPost("character-backstory")]
    public async Task<IActionResult> GenerateCharacterBackstory([FromBody] AIDataGeneratorInput input)
    {
        var command = input.ToCommand();
        var backstory = await _generateCharacterBackstory.HandleAsync(command);
        
        return Ok(backstory);
    }
    
    [HttpGet("character-names")]
    public async Task<IActionResult> GenerateCharacterNames()
    {
        var names = await _generateCharacterNames.HandleAsync(new AIDataGeneratorCommand());
        
        return Ok(names);
    }
    
    [HttpGet("campaign-backstory")]
    public async Task<IActionResult> GenerateCampaignBackstory()
    {
        var backstory = await _generateCampaignBackstory.HandleAsync(new AIDataGeneratorCommand());
        
        return Ok(backstory);
    }
}