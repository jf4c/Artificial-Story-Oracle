using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Players.GetByUserId;

namespace ASO.Application.Abstractions.UseCase.Players;

public interface IGetPlayerByUserIdHandler : IQueryHandler<GetPlayerByUserIdQuery, GetPlayerByUserIdResponse>
{
}