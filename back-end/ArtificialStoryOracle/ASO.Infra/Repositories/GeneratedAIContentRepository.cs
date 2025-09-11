using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.AI.Entities;
using ASO.Infra.Database;

namespace ASO.Infra.Repositories;

public class GeneratedAIContentRepository(AppDbContext context) : IGeneratedAIContentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<GeneratedAIContent> Create(GeneratedAIContent content)
    {
        _context.GeneratedAIContents.Add(content);
        await _context.SaveChangesAsync();
        return content;
    }
}