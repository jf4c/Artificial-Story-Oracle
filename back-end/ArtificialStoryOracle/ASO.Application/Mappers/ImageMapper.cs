using ASO.Application.UseCases.Images.GetAll;
using ASO.Domain.Game.Entities;

namespace ASO.Application.Mappers;

public static class ImageMapper
{
    public static GetAllImagesResponse ToGetAllImagesResponse(this IEnumerable<Image> images)
    {
        return new GetAllImagesResponse
        {
            Images = images.Select(image => new ImageDto()
            {
                Id = image.Id,
                Name = image.Name,
                Url = image.Url,
                Description = image.Description
            }).ToList()
        };
    }
}