﻿using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.Mappers;
using ASO.Domain.Identity.Repositories.Abstractions;

namespace ASO.Application.UseCases.Players.GetByUserId;

public sealed class GetPlayerByUserIdHandler(IPlayerUserRepository playerUserRepository) : IGetPlayerByUserIdHandler
{
    private readonly IPlayerUserRepository _playerUserRepository = playerUserRepository;
    
    public async Task<GetPlayerByUserIdResponse?> Handle(GetPlayerByUserIdQuery request)
    {
        var player = await _playerUserRepository.GetByUserId(request.KeycloakUserId);
        
        if (player is null)
            return null;

        var response = player.ToGetPlayerByUserIdResponse();
        
        return response;
    }
}