namespace ASO.Api.Inputs;

public sealed record SendFriendRequestInput
{
    public required Guid AddresseeId { get; init; }
}

