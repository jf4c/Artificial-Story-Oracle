using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Domain.Game.Repositories.Abstractions;

namespace ASO.Application.UseCases.Characters.GetAll;

public sealed class GetAllCharactersHandler(ICharacterRepository characterRepository)
    : IGetAllCharactersHandler
{
    private readonly ICharacterRepository _characterRepository = characterRepository;

    public async Task<GetAllCharactersResponse> Handle(GetAllCharactersQuery request)
    {
        var characters = await _characterRepository.GetAll();
        var response = new GetAllCharactersResponse(characters);
        return response;
    }
}