using ASO.Api.Inputs;
using ASO.Application.UseCases.Campaigns.Complete;
using ASO.Application.UseCases.Campaigns.Create;
using ASO.Application.UseCases.Campaigns.Delete;
using ASO.Application.UseCases.Campaigns.GetById;
using ASO.Application.UseCases.Campaigns.GetMyCampaigns;
using ASO.Application.UseCases.Campaigns.Pause;
using ASO.Application.UseCases.Campaigns.Resume;
using ASO.Application.UseCases.Campaigns.Start;
using ASO.Application.UseCases.Campaigns.Update;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Game.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class CampaignController(
    CreateCampaignHandler createHandler,
    GetCampaignByIdHandler getByIdHandler,
    GetMyCampaignsHandler getMyCampaignsHandler,
    UpdateCampaignHandler updateHandler,
    DeleteCampaignHandler deleteHandler,
    StartCampaignHandler startHandler,
    PauseCampaignHandler pauseHandler,
    ResumeCampaignHandler resumeHandler,
    CompleteCampaignHandler completeHandler,
    IPlayerRepository playerRepository) : ControllerBase
{
    private readonly CreateCampaignHandler _createHandler = createHandler;
    private readonly GetCampaignByIdHandler _getByIdHandler = getByIdHandler;
    private readonly GetMyCampaignsHandler _getMyCampaignsHandler = getMyCampaignsHandler;
    private readonly UpdateCampaignHandler _updateHandler = updateHandler;
    private readonly DeleteCampaignHandler _deleteHandler = deleteHandler;
    private readonly StartCampaignHandler _startHandler = startHandler;
    private readonly PauseCampaignHandler _pauseHandler = pauseHandler;
    private readonly ResumeCampaignHandler _resumeHandler = resumeHandler;
    private readonly CompleteCampaignHandler _completeHandler = completeHandler;
    private readonly IPlayerRepository _playerRepository = playerRepository;

    private async Task<Guid> GetCurrentPlayerIdAsync()
    {
        var keycloakUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("sub")?.Value
                          ?? throw new UnauthorizedAccessException("User ID não encontrado no token.");

        if (!Guid.TryParse(keycloakUserId, out var userId))
            throw new UnauthorizedAccessException("User ID inválido.");

        var player = await _playerRepository.GetByKeycloakUserIdAsync(userId)
            ?? throw new InvalidOperationException("Player não encontrado.");

        return player.Id;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignInput input)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new CreateCampaignCommand(playerId, input.Name, input.Description, input.MaxPlayers, input.IsPublic);
        var response = await _createHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetCampaignById), new { campaignId = response.Id }, response);
    }

    [HttpGet("{campaignId}")]
    public async Task<IActionResult> GetCampaignById(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var query = new GetCampaignByIdQuery(playerId, campaignId);
        var response = await _getByIdHandler.HandleAsync(query);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyCampaigns([FromQuery] CampaignStatus? status = null)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var query = new GetMyCampaignsQuery(playerId, status);
        var response = await _getMyCampaignsHandler.HandleAsync(query);
        return Ok(response);
    }

    [HttpPut("{campaignId}")]
    public async Task<IActionResult> UpdateCampaign(Guid campaignId, [FromBody] UpdateCampaignInput input)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new UpdateCampaignCommand(playerId, campaignId, input.Name, input.Description, input.MaxPlayers, input.Status);
        await _updateHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{campaignId}")]
    public async Task<IActionResult> DeleteCampaign(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new DeleteCampaignCommand(playerId, campaignId);
        await _deleteHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPost("{campaignId}/start")]
    public async Task<IActionResult> StartCampaign(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new StartCampaignCommand(playerId, campaignId);
        await _startHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPost("{campaignId}/pause")]
    public async Task<IActionResult> PauseCampaign(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new PauseCampaignCommand(playerId, campaignId);
        await _pauseHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPost("{campaignId}/resume")]
    public async Task<IActionResult> ResumeCampaign(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new ResumeCampaignCommand(playerId, campaignId);
        await _resumeHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPost("{campaignId}/complete")]
    public async Task<IActionResult> CompleteCampaign(Guid campaignId)
    {
        var playerId = await GetCurrentPlayerIdAsync();
        var command = new CompleteCampaignCommand(playerId, campaignId);
        await _completeHandler.HandleAsync(command);
        return NoContent();
    }
}

