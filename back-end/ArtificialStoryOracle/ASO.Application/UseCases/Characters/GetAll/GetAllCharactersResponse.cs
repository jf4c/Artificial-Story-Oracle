using ASO.Application.Abstractions.Shared;
using ASO.Domain.Game.Entities;

namespace ASO.Application.UseCases.Characters.GetAll;

public sealed record GetAllCharactersResponse : IResponse
{
    public GetAllCharactersResponse(IEnumerable<Character> characters)
    {
        
    }
    
    
}