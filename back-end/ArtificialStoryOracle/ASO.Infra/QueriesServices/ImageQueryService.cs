using ASO.Domain.Game.Abstractions.QueriesServices;
using ASO.Domain.Game.Entities;
using ASO.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.QueriesServices;

public class ImageQueryService(AppDbContext context) : IImageQueryService
{
    private readonly AppDbContext _context = context;


    public async Task<List<Image>> GetAllAsync()
    {
        return await _context.Images
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Image> GetById(Guid id)
    {
        var image = await _context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
        
        if (image == null)
            throw new KeyNotFoundException($"Image with ID {id} not found.");
        
        return image;
    }
}