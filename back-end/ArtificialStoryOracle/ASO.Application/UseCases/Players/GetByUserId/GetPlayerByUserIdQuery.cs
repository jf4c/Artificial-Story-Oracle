using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Players.GetByUserId;

public sealed record GetPlayerByUserIdQuery(Guid KeycloakUserId) : IQuery;