using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.SearchPlayers;

public sealed record SearchPlayersResponse : IResponse
{
    public required Guid Id { get; init; }
    public required string NickName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string? FriendshipStatus { get; init; }
}

