using ASO.Application.Abstractions.UseCase.Players;
using ASO.Application.Mappers;
using ASO.Domain.Identity.Entities;
using ASO.Domain.Identity.Repositories.Abstractions;
using ASO.Domain.Shared.Abstractions;

namespace ASO.Application.UseCases.Players.Create;

public sealed class CreatePlayerHandler(
    IPlayerUserRepository playerUserRepository,
    IUnitOfWork unitOfWork) : ICreatePlayerHandler
{
    private readonly IPlayerUserRepository _playerUserRepository = playerUserRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreatePlayerResponse> HandleAsync(CreatePlayerCommand command)
    {
        var request = command.ToPlayerUserDto();
        
        var player = PlayerUser.Create(request);  //TODO: criar evento de dominio ✅ FEITO
        
        await _playerUserRepository.Create(player); 
        
        // Salvar e disparar eventos de domínio
        await _unitOfWork.SaveChangesAsync();

        var response = player.ToCreatePlayerResponse();

        return response;
    }
}