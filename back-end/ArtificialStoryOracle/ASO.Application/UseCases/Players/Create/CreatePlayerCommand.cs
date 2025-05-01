using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Players.Create;

public sealed record CreatePlayerCommand : ICommand
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string NickName { get; init; } = string.Empty;
    public Guid KeycloakUserId { get; init; }
    
    public CreatePlayerCommand(string name, string email, string nickName, string keycloakUserId)
    {
        Name = name;
        Email = email;
        NickName = nickName;
        KeycloakUserId = Guid.Parse(keycloakUserId);
    }
}