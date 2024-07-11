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
        var history = new UserImageHistory
        {
            UserId = userId,
            ImageId = imageId,
            ViewedAt = DateTime.Now
        };
        _context.History.Add(history);
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

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
