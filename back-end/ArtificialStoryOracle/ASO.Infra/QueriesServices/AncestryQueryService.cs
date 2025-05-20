using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class AncestryQueryService(AppDbContext context) : IAncestryQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<IEnumerable<Ancestry>>  GetAllAncestries()
    {
        var ancestries = await _context.Ancestries.ToListAsync();
        
        if (ancestries == null || !ancestries.Any())
            throw new Exception("No ancestries found");
        
        return ancestries;
    }
}