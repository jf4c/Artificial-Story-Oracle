using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Players.Create;

namespace ASO.Application.Abstractions.UseCase.Players;

public interface ICreatePlayerHandler : ICommandHandler<CreatePlayerCommand, CreatePlayerResponse>;