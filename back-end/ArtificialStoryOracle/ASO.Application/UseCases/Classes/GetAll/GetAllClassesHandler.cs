using ASO.Application.Abstractions.UseCase.Classes;
using ASO.Application.Mappers;
using ASO.Domain.Game.QueriesServices;

namespace ASO.Application.UseCases.Classes.GetAll;

public sealed class GetAllClassesHandler(IClassQueryService classQueryService)
    : IGetAllClassesHandler
{
    private readonly IClassQueryService _classQueryService = classQueryService;

    public async Task<GetAllClassesResponse> Handle()
    {
        var classes = await _classQueryService.GetAll();
        
        return classes.ToGetAllClassesResponse();
    }
}