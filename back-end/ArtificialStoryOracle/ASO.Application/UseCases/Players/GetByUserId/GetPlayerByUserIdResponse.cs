using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Players.GetByUserId;

public sealed record GetPlayerByUserIdResponse : IResponse
{
    public string Email { get; init; } = string.Empty;
    public string NickName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}