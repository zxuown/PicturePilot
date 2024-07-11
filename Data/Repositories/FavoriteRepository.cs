using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class FavoriteRepository(PicturesDbContext context)
{
    private readonly PicturesDbContext _context = context;

    public async Task AddAsync(Favorite entity)
    {
        await _context.Favorites.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int imageId)
    {
        var item = await GetByIdsAsync(userId,imageId);
        _context.Favorites.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync()
    {
        return await _context.Favorites.AsNoTracking().ToListAsync();
    }

    public async Task<Favorite> GetByIdsAsync(int userId, int imageId)
    {
        return await _context.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && imageId == x.ImageId);
    }

    public async Task UpdateAsync(Favorite entity)
    {
        _context.Favorites.Update(entity);
        _context.SaveChanges();
    }
}
