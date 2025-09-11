using ASO.Domain.Game.Abstractions.QueriesServices;
using ASO.Domain.Game.Entities;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class ClassQueryService(AppDbContext context) : IClassQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<Class> GetById(Guid id)
    {
        var @class = await _context.Classes
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (@class == null)
            throw new Exception($"Class with id {id} not found");
        
        return @class;
    }

    public async Task<List<Class>> GetByIds(List<Guid> ids)
    {
        var classes = await _context.Classes
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
        
        if (classes == null)
            throw new Exception("No classes found");
        
        return classes;
    }

    public async Task<List<Class>> GetAll()
    {
        return await _context.Classes
            .AsNoTracking()
            .ToListAsync();
    }
}