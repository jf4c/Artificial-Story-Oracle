using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.SearchPlayers;

public sealed record SearchPlayersQuery(string SearchTerm) : IQuery;

