﻿using ASO.Application.UseCases.Players.Create;
using ASO.Application.UseCases.Players.GetByUserId;
using ASO.Domain.Game.Entities;
using ASO.Domain.Identity.Dtos;
using ASO.Domain.Identity.Entities;

namespace ASO.Application.Mappers;

public static class PlayerUserMapper
{
    
    public static CreatePlayerResponse ToCreatePlayerResponse(this PlayerUser player)
    {
        return new CreatePlayerResponse
        {
            //TODO: Criar resposta
        };
    }
    
    public static PlayerRequest ToPlayerUserDto(this CreatePlayerCommand command)
    {
        return new PlayerRequest
        {
            Name = command.Name,
            NickName = command.NickName,
            Email = command.Email,
            KeycloakUserId = command.KeycloakUserId
        };
    }
    
    public static GetPlayerByUserIdResponse ToGetPlayerByUserIdResponse(this PlayerUser player)
    {
        return new GetPlayerByUserIdResponse
        {
            Email = player.Email.Address,
            NickName = player.NickName.Nick,
            FirstName = player.Name.FirstName,
            LastName = player.Name.LastName
        };
    }
}