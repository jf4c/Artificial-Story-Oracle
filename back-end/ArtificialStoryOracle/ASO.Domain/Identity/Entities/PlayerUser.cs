﻿using ASO.Domain.Identity.Dtos;
using ASO.Domain.Identity.Events;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Identity.Entities;

public sealed class PlayerUser : Entity, IAggragateRoot
{
    // Construtor privado sem parâmetros para o EF Core
    private PlayerUser()
    {
        Name = null!;
        Email = null!;
        NickName = null!;
    }
    
    private PlayerUser(PlayerRequest request)
    {
        Name = Name.Create(request.Name, "Default");
        Email = Email.Create(request.Email);
        NickName = Nickname.Create(request.NickName);
        KeycloakUserId = request.KeycloakUserId;
    }
    
    public static PlayerUser Create(PlayerRequest request)
    {
        var playerUser = new PlayerUser(request);
        
        // Levantar evento de domínio para criar Player no contexto Game
        playerUser.RaiseEvent(new PlayerUserCreatedEvent
        {
            KeycloakUserId = playerUser.KeycloakUserId,
            Name = $"{playerUser.Name.FirstName} {playerUser.Name.LastName}",
            Email = playerUser.Email.Address,
            NickName = playerUser.NickName.Nick
        });
        
        return playerUser;
    }
    
    public Guid KeycloakUserId { get; }
    public Email Email { get; }
    public Nickname NickName { get; }
    public Name Name { get; }
}