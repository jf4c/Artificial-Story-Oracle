using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Characters.GetAll;

namespace ASO.Application.Abstractions.UseCase.Characters;

public interface IGetAllCharactersHandler : IQueryPaginatedHandler<GetAllCharactersFilter, GetAllCharactersResponse>;