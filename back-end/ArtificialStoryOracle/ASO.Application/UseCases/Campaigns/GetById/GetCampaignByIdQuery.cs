using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Campaigns.GetById;

public sealed record GetCampaignByIdQuery(Guid CurrentPlayerId, Guid CampaignId) : IQuery;

