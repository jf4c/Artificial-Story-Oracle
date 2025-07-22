using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Images.GetAll;

namespace ASO.Application.Abstractions.UseCase.Images;

public interface IGetAllImagesHandler : IQueryHandler<GetAllImagesResponse>;