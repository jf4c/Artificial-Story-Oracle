using ASO.Application.Abstractions.UseCase.Images;
using ASO.Application.Mappers;
using ASO.Domain.Game.QueriesServices;

namespace ASO.Application.UseCases.Images.GetAll;

public class GetAllImagesHandler(IImageQueryService imageQueryService) : IGetAllImagesHandler
{
    private readonly IImageQueryService _imageQueryService = imageQueryService;

    public async Task<GetAllImagesResponse> Handle()
    {
        var images = await _imageQueryService.GetAllAsync();

        return images.ToGetAllImagesResponse();
    }
}