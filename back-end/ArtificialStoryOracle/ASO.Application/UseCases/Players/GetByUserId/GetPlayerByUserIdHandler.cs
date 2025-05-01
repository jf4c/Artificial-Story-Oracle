using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.Mappers;
using ASO.Domain.Identity.Repositories.Abstractions;

namespace ASO.Application.UseCases.Players.GetByUserId;

public class GetPlayerByUserIdHandler(IPlayerUserRepository playerUserRepository) : IGetPlayerByUserIdHandler
{
    private readonly IPlayerUserRepository _playerUserRepository = playerUserRepository;
    
    public async Task<GetPlayerByUserIdResponse> Handle(GetPlayerByUserIdQuery request)
    {
        var player = await _playerUserRepository.GetByUserId(request.KeycloakUserId);
        
        //Todo: Exeption generica
        if (player is null)
            throw new Exception("Player not found");

        var response = player.ToGetPlayerByUserIdResponse();
        
        return response;
    }
}