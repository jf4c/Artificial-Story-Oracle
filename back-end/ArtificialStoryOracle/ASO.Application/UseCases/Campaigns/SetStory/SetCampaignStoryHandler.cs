using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Abstractions.Repositories;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Campaigns.SetStory;

public sealed class SetCampaignStoryHandler(
    ICampaignRepository campaignRepository,
    IUnitOfWork unitOfWork) : ICommandHandlerAsync<SetCampaignStoryCommand>
{
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(SetCampaignStoryCommand command)
    {
        var campaign = await _campaignRepository.GetByIdAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        if (campaign.CreatorId != command.CurrentPlayerId)
            throw new UnauthorizedAccessException("Apenas o criador da campanha pode definir a história introdutória.");

        campaign.SetStoryIntroduction(command.StoryIntroduction);

        await _unitOfWork.SaveChangesAsync();
    }
}
