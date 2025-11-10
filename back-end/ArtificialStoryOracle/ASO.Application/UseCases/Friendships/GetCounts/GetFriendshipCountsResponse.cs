using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Friendships.GetCounts;

public sealed record GetFriendshipCountsResponse : IResponse
{
    public required int TotalFriends { get; init; }
    public required int PendingReceived { get; init; }
    public required int PendingSent { get; init; }
}

