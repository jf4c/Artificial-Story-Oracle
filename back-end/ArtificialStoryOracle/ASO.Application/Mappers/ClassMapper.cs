using ASO.Application.UseCases.Classes.GetAll;
using ASO.Domain.Game.Entities;

namespace ASO.Application.Mappers;

public static class ClassMapper
{
    public static GetAllClassesResponse ToGetAllClassesResponse(this IEnumerable<Class> ancestries)
    {
        return new GetAllClassesResponse
        {
            Classes = ancestries.Select(a => new ClassDto
            {
                Id = a.Id,
                Name = a.Name
            })
        };
    }
}