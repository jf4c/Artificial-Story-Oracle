using ASO.Domain.Shared.Aggregates.Abstractions;

namespace ASO.Domain.Shared.Repositories.Abstractions;

public interface IRepository<TEntity> where TEntity : IAggragateRoot;