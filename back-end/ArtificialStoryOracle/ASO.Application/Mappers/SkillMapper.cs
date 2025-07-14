using ASO.Application.UseCases.Ancestry.GetAllAncestry;
using ASO.Application.UseCases.Skills.GetAllSkills;
using ASO.Domain.Game.Entities;

namespace ASO.Application.Mappers;

public static class SkillMapper
{
    public static GetAllSkillsResponse ToGetAllSkillsResponse(this IEnumerable<Skill> skills)
    {
        return new GetAllSkillsResponse
        {
            Skills = skills.Select(a => new SkillDto()
            {
                Id = a.Id,
                Name = a.Name,
            })
        };
    }
}