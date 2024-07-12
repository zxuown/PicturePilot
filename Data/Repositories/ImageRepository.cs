using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class ImageRepository(PicturesDbContext context) : BaseRepository<Image>(context)
{
    public async new Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _entities.Include(x => x.User).Include(x => x.Tags).ToListAsync();
    }

    public async new Task<Image> GetByIdAsync(int id)
    {
        return await _entities.Include(x => x.User).Include(x => x.Tags).Include(x=> x.Favorites).Include(x=> x.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Image>> GetAllBlockedAsync()
    {
        var allImages = await GetAllAsync();
        return allImages.Where(x => x.IsBLocked).ToList();
    }

    public async Task<IEnumerable<Image>> GetAllByUserIdAsync(int userId)
    {
        return await _entities.Where(x => x.User.Id == userId).ToListAsync();
    }

    public async Task<int> GetFavoritesCountAsync(int imageId)
    {
        return await _context.Favorites.Where(x => x.ImageId == imageId).CountAsync();
    }
}
