using ASO.Domain.Identity.Dtos;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Identity.Entities;

public sealed class PlayerUser : Entity, IAggragateRoot
{
    private PlayerUser(PlayerRequest request) : base(Guid.NewGuid())
    {
        Name = Name.Create(request.Name, "Default");
        Email = Email.Create(request.Email);
        NickName = Nickname.Create(request.NickName);
        KeycloakUserId = request.KeycloakUserId;
    }
    
    public static PlayerUser Create(PlayerRequest request) 
        => new(request);
    
    public Guid KeycloakUserId { get; }
    public Email Email { get; }
    public Nickname NickName { get; }
    public Name Name { get; }
}