using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class UserRepository(UserManager<User> userManager, PicturesDbContext context)
{
    private readonly UserManager<User> _userManager = userManager;

    private readonly PicturesDbContext _context = context;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllBlockedAsync()
    {
        return await _userManager.Users.Where(x => x.IsBLocked).ToListAsync();
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public void AddToHistory(int userId, int imageId)
    {

        var history = _context.History.FirstOrDefault(x => x.UserId == userId && x.ImageId == imageId);
        if (history != null)
        {
            history.ViewedAt = DateTime.Now;
            _context.History.Update(history);
        }
        else
        {
            history = new UserImageHistory
            {
                UserId = userId,
                ImageId = imageId,
                ViewedAt = DateTime.Now
            };
            _context.History.Add(history);
        }
        _context.SaveChanges();
    }

    public async Task<List<UserImageHistory>> GetHistoryAsync(int userId)
    {
        return await _context.History.Include(x=> x.Image).Include(x=> x.User).Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task ClearHistory(int userId, DateTime cutoffDate)
    {
        var history = await _context.History.Where(x => x.UserId == userId && x.ViewedAt > cutoffDate).ToListAsync();
        _context.History.RemoveRange(history);
        _context.SaveChanges();
    }

    public bool SwitchFavorite(int userId, int imageId)
    {
        var favorite = _context.Favorites.FirstOrDefault(x => x.UserId == userId && x.ImageId == imageId);
        bool isFavorite = favorite != null;
        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
        }
        else
        {
            favorite = new Favorite
            {
                UserId = userId,
                ImageId = imageId
            };
            _context.Favorites.Add(favorite);
        }
        _context.SaveChanges();
        return !isFavorite;
    }

    public bool IsFavorite(int userId, int imageId)
    {
        return _context.Favorites.Any(x => x.UserId == userId && x.ImageId == imageId);
    }

    public async Task<List<Image>> GetFavoritesAsync(int userId)
    {
        return await _context.Favorites.Include(x => x.Image).Where(x => x.UserId == userId).Select(x => x.Image).ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
