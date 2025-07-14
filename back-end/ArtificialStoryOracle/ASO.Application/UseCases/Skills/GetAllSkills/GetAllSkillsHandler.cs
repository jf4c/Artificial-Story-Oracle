using ASO.Application.Abstractions.UseCase.Skills;
using ASO.Application.Mappers;
using ASO.Domain.Game.QueriesServices;

namespace ASO.Application.UseCases.Skills.GetAllSkills;

public sealed class GetAllSkillsHandler(ISkillQueryService skillQueryService) : IGetAllSkillsHandler
{
    private readonly ISkillQueryService _skillQueryService = skillQueryService;

    public async Task<GetAllSkillsResponse> Handle()
    {
        var skills = await _skillQueryService.GetAll();
        
        return skills.ToGetAllSkillsResponse();
    }
}