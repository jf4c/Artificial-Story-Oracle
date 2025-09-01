using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Repositories.Abstractions;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Repositories;

public class CharacterRepository(AppDbContext context) : ICharacterRepository
{
    private readonly AppDbContext _context = context;
    
    public async Task<Character> Create(Character character)
    {
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        return character;
    }

    public IQueryable<Character> GetAll()
    {
        return _context.Characters
            .AsNoTracking()
            .Include(c => c.Image)
            .Include(c => c.Ancestry)
            .Include(c => c.Classes);
    }
}