using ASO.Api.Inputs;
using ASO.Api.Inputs.Mappers;
using ASO.Application.Abstractions.UseCase.Oracle;
using ASO.Application.UseCases.Oracle;
using ASO.Application.UseCases.Oracle.GenerateCampaignStory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OracleController(
    IGenerateCharacterBackstory generateCharacterBackstory,
    IGenerateCharactersNames generateCharactersNames,
    IGenerateCampaignBackstory generateCampaignBackstory,
    GenerateCampaignStoryHandler generateCampaignStoryHandler,
    GenerateCampaignStoryFromCharactersHandler generateCampaignStoryFromCharactersHandler
    ) : ControllerBase
{
    private readonly IGenerateCharacterBackstory _generateCharacterBackstory = generateCharacterBackstory;
    private readonly IGenerateCharactersNames _generateCharacterNames = generateCharactersNames;
    private readonly IGenerateCampaignBackstory _generateCampaignBackstory = generateCampaignBackstory;
    private readonly GenerateCampaignStoryHandler _generateCampaignStoryHandler = generateCampaignStoryHandler;
    private readonly GenerateCampaignStoryFromCharactersHandler _generateCampaignStoryFromCharactersHandler = generateCampaignStoryFromCharactersHandler;
    
    [HttpPost("character-backstory")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateCharacterBackstory([FromBody] AIDataGeneratorInput input)
    {
        var command = input.ToCommand();
        var backstory = await _generateCharacterBackstory.HandleAsync(command);
        
        return Ok(backstory);
    }
    
    [HttpGet("character-names")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateCharacterNames([FromQuery] GenerateCharacterNamesInput input)
    {
        var command = input.ToCommand();
        var names = await _generateCharacterNames.HandleAsync(command);
        
        return Ok(names);
    }
    
    [HttpGet("campaign-backstory")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateCampaignBackstory()
    {
        var backstory = await _generateCampaignBackstory.HandleAsync(new AIDataGeneratorCommand());
        
        return Ok(backstory);
    }

    [HttpPost("campaign-story")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateCampaignStory([FromBody] GenerateCampaignStoryInput input)
    {
        var command = new GenerateCampaignStoryCommand(input.CampaignId);
        var story = await _generateCampaignStoryHandler.HandleAsync(command);
        
        return Ok(story);
    }

    [HttpPost("campaign-story-from-characters")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateCampaignStoryFromCharacters([FromBody] GenerateCampaignStoryFromCharactersInput input)
    {
        var command = new GenerateCampaignStoryFromCharactersCommand(
            input.CharacterIds,
            input.CampaignName,
            input.CampaignDescription
        );
        var story = await _generateCampaignStoryFromCharactersHandler.HandleAsync(command);
        
        return Ok(story);
    }
}