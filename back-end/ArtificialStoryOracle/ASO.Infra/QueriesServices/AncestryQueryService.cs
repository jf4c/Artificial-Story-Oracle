using ASO.Domain.Game.Entities;
using ASO.Domain.Game.QueriesServices;
using ASO.Domain.Shared.Exceptions;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class AncestryQueryService(AppDbContext context) : IAncestryQueryService
{
    private readonly AppDbContext _context = context;
    
    public async Task<IEnumerable<Ancestry>>  GetAll()
    {
        var ancestries = await _context.Ancestries.ToListAsync();
        
        if (ancestries.Count == 0)
            throw new AncestriesNotFoundException();
        
        return ancestries;
    }

    public async Task<Ancestry> GetById(Guid id)
    {
        if (id == Guid.Empty)
            throw new ValidationException("O ID da ancestralidade não pode ser vazio.");
        
        var ancestry = await _context.Ancestries.FirstOrDefaultAsync(x => x.Id == id);
        
        if (ancestry == null)
            throw new AncestriesNotFoundException(id);
        
        return ancestry;
    }
}