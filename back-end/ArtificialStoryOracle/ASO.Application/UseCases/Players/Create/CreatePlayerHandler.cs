using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.Mappers;
using ASO.Domain.Game.Entities;
using ASO.Domain.Identity.Entities;
using ASO.Domain.Identity.Repositories.Abstractions;

namespace ASO.Application.UseCases.Players.Create;

public sealed class CreatePlayerHandler(IPlayerUserRepository playerUserRepository) : ICreatePlayerHandler
{
    private readonly IPlayerUserRepository _playerUserRepository = playerUserRepository;

    public CreatePlayerResponse Handle(CreatePlayerCommand command)
    {
        var request = command.ToPlayerUserDto();
        
        var palyer = PlayerUser.Create(request);  //TODO: criar evento de dominio
        
        _playerUserRepository.Create(palyer); 

        var response = palyer.ToCreatePlayerResponse();

        return response;
    }
}