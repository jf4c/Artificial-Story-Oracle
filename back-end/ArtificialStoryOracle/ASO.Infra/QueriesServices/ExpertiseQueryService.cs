using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class ExpertiseQueryService(AppDbContext context) : IExpertiseQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<Expertise> GetById(Guid id)
    {
        var expertise = await _context.Expertises.FirstOrDefaultAsync(x => x.Id == id);
        
        if (expertise == null)
            throw new Exception($"Expertise with id {id} not found");
        
        return expertise;
    }

    public async Task<List<Expertise>> GetByIds(List<Guid> ids)
    {
        var expertises = await _context.Expertises.Where(x => ids.Contains(x.Id)).ToListAsync();
        
        if (expertises == null)
            throw new Exception("No expertises found");
        
        return expertises;
    }
}