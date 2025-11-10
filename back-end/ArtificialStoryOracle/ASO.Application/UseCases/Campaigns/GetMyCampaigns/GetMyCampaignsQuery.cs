using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Enums;

namespace ASO.Application.UseCases.Campaigns.GetMyCampaigns;

public sealed record GetMyCampaignsQuery(Guid CurrentPlayerId, CampaignStatus? Status = null) : IQuery;

