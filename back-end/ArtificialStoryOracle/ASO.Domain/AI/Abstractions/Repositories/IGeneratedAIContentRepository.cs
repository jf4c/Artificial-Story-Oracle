using ASO.Domain.AI.Entities;
using ASO.Domain.Shared.Repositories.Abstractions;

namespace ASO.Domain.AI.Abstractions.Repositories;

public interface IGeneratedAIContentRepository : IRepository<GeneratedAIContent>
{
    Task<GeneratedAIContent> Create(GeneratedAIContent content);
}