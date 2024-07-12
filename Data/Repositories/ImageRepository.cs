using Microsoft.EntityFrameworkCore;
using PicturePilot.Business.Models;
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
        return await _entities.Include(x => x.User)
            .Include(x => x.Tags)
            .Include(x => x.Favorites)
            .Include(x => x.Comments)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Image>> GetAllBlockedAsync()
    {
        var allImages = await GetAllAsync();
        return allImages.Where(x => x.IsBLocked).ToList();
    }

    public async Task<IQueryable<Image>> GetAllUnblockedAsync()
    {
        var allImages = await GetAllAsync();
        return _entities.Include(x => x.User).Include(x => x.Tags).Where(x => !x.IsBLocked);
    }

    public async Task<IEnumerable<Image>> GetAllByUserIdAsync(int userId)
    {
        return await _entities.Where(x => x.User.Id == userId).ToListAsync();
    }

    public async Task<int> GetFavoritesCountAsync(int imageId)
    {
        return await _context.Favorites.Where(x => x.ImageId == imageId).CountAsync();
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Image>> SearchAsync(SearchModel search)
    {
        var query = await GetAllUnblockedAsync();
        if(search.Query != null)
        {
            query = query.Where(x => x.Title.Contains(search.Query));
        }
        if(search.Tags != null && search.Tags.Count > 0)
        { 
            query = query.Where(x => x.Tags.Any(t => search.Tags.Contains(t.Title)));
        }
        return await query.ToListAsync();
    }

    public IEnumerable<string> GetTags()
    {
        return _context.Tags.Select(x => x.Title).ToList();
    }
}
