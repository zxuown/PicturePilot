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

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
