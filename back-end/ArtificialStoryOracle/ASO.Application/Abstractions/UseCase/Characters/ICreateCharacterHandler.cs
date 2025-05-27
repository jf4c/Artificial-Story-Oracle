using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Characters.Create;

namespace ASO.Application.Abstractions.UseCase.Characters;

public interface ICreateCharacterHandler 
    : ICommandHandlerAsync<CreateCharacterCommand, CreateCharacterResponse>;