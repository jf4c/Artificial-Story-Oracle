using ASO.Application.Abstractions.UseCase.Characters;
using ASO.Application.Mappers;
using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Domain.Game.Repositories.Abstractions;

namespace ASO.Application.UseCases.Characters.Create;

public sealed class CreateCharacterHandler(
    ICharacterRepository repository, 
    IAncestryQueryService ancestryQueryService,
    IClassQueryService classQueryService,
    IExpertiseQueryService expertiseQueryService
    ) : ICreateCharacterHandler
{
    private readonly ICharacterRepository _repository = repository;
    private readonly IAncestryQueryService _ancestryQueryService = ancestryQueryService;
    private readonly IClassQueryService _classQueryService = classQueryService;
    private readonly IExpertiseQueryService _expertiseQueryService = expertiseQueryService;
    
    public async Task<CreateCharacterResponse> HandleAsync(CreateCharacterCommand command)
    {
        var ancestry = await _ancestryQueryService.GetById(command.AncestryId);
        var classes = await _classQueryService.GetByIds(command.ClassesIds);
        var expertises = await _expertiseQueryService.GetByIds(command.ExpertisesIds);
        
        var dto = command.ToCreateCharacterDto(ancestry, classes, expertises);
        
        var character = Character.Create(dto);
        
        await _repository.Create(character);

        return character.ToCreateCharacterResponse();
    }
}