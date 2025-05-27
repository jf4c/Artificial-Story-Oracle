using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class AncestryQueryService(AppDbContext context) : IAncestryQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<IEnumerable<Ancestry>>  GetAll()
    {
        var ancestries = await _context.Ancestries.ToListAsync();
        
        if (ancestries == null || !ancestries.Any())
            throw new Exception("No ancestries found");
        
        return ancestries;
    }

    public async Task<Ancestry> GetById(Guid id)
    {
        var ancestry = await _context.Ancestries.FirstOrDefaultAsync(x => x.Id == id);
        
        if (ancestry == null)
            throw new Exception($"Ancestry with id {id} not found");
        
        return ancestry;
    }
}