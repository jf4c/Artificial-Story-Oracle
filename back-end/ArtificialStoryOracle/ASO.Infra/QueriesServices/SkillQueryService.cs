using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Domain.Shared.Exceptions;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class SkillQueryService(AppDbContext context) : ISkillQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<List<Skill>> GetAll()
    {
        var expertises = await _context.Skill
            .AsNoTracking()
            .ToListAsync();
        
        if (expertises == null || expertises.Count == 0)
            throw new SkillsNotFoundException();
        
        return expertises;
    }
    public async Task<Skill> GetById(Guid id)
    {
        var expertise = await _context.Skill
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (expertise == null)
            throw new Exception($"Expertise with id {id} not found");
        
        return expertise;
    }

    public async Task<List<Skill>> GetByIds(List<Guid> ids)
    {
        var expertises = await _context.Skill
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
        
        if (expertises == null)
            throw new Exception("No expertises found");
        
        return expertises;
    }
}