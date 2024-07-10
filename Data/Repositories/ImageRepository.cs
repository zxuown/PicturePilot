using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;
using System.Collections.Immutable;

namespace PicturePilot.Data.Repositories;

public class ImageRepository(PicturesDbContext context) : BaseRepository<Image>(context)
{
    public async new Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _entities.Include(x => x.user).Include(x => x.Tags).ToListAsync();
    }

    public async Task<IEnumerable<Image>> GetAllBlockedAsync()
    {
        var allImages = await GetAllAsync();
        return allImages.Where(x => x.IsBLocked).ToList();
    }
}
