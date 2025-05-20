using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Ancestry.GetAllAncestry;

namespace ASO.Application.Abstractions.UseCase.Ancestry;

public interface IGetAllAncestryHandler : IQueryHandler<GetAllAncestryResponse>;