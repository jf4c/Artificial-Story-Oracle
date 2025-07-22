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
    ISkillQueryService skillQueryService,
    IImageQueryService imageQueryService
    ) : ICreateCharacterHandler
{
    private readonly ICharacterRepository _repository = repository;
    private readonly IAncestryQueryService _ancestryQueryService = ancestryQueryService;
    private readonly IClassQueryService _classQueryService = classQueryService;
    private readonly ISkillQueryService _skillQueryService = skillQueryService;
    private readonly IImageQueryService _imageQueryService = imageQueryService;
    
    public async Task<CreateCharacterResponse> HandleAsync(CreateCharacterCommand command)
    {
        var ancestry = await _ancestryQueryService.GetById(command.AncestryId);
        var classes = await _classQueryService.GetById(command.ClasseId);
        var expertises = await _skillQueryService.GetByIds(command.SkillsIds);
        var image = await _imageQueryService.GetById(command.ImageId);
        
        var dto = command.ToCreateCharacterDto(ancestry, classes, expertises, image);
        
        var character = Character.Create(dto);
        
        await _repository.Create(character);

        return character.ToCreateCharacterResponse();
    }
}