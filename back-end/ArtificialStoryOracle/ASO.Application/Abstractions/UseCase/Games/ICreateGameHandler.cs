using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Games.Create;

namespace ASO.Application.Abstractions.UseCase.Games;

public interface ICreateGameHandler : ICommandHandler<CreateGameCommand, CreateGameResponse>
{
    
}