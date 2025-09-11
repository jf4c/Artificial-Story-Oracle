using ASO.Domain.Game.Entities;
using ASO.Domain.Shared.QueriesServices.Abstractions;

namespace ASO.Domain.Game.Abstractions.QueriesServices;

public interface IImageQueryService : IQueryService<Image>
{
    Task<List<Image>> GetAllAsync();
    Task<Image> GetById(Guid id);
}