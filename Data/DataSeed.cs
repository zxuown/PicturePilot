using Microsoft.AspNetCore.Identity;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data
{
    public class DataSeed(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, PicturesDbContext context)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager = roleManager;
        private readonly PicturesDbContext _context = context;

        public List<User> GetDefaultUsers()
        {
            return new List<User>
            {
                new User
                {
                    Name = "Test Admin",
                    UserName = "Admin",
                    Email = "smtp2807@gmail.com",
                    AvatarUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTyGo8XzszXwJThST5wxfqGFehUkRrVS6Njdw&s",
                    Login = "Admin",
                    IsBLocked = false,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Name = "Test User",
                    UserName = "User",
                    Email = "test1234@gmail.com",
                    AvatarUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTyGo8XzszXwJThST5wxfqGFehUkRrVS6Njdw&s",
                    Login = "User",
                    IsBLocked = false,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Name = "Test User 2",
                    UserName = "User2",
                    Email = "test1@gmail.com",
                    AvatarUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTyGo8XzszXwJThST5wxfqGFehUkRrVS6Njdw&s",
                    Login = "User2",
                    IsBLocked = false,
                    CreatedAt = DateTime.Now

                }

            };
        }

        public List<IdentityRole<int>> GetDefaultRoles()
        {
            return new List<IdentityRole<int>>
            {
                new IdentityRole<int>
                {
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<int>
                {
                    Name = "USER",
                    NormalizedName = "USER"
                }
            };
        }

        public async Task SeedData()
        {
            var roles = GetDefaultRoles();
            _context.UserRoles.RemoveRange(_context.UserRoles);
            _context.Users.RemoveRange(_context.Users);
            _context.Roles.RemoveRange(_context.Roles);
            await _context.SaveChangesAsync();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role.Name))
                {
                    await _roleManager.CreateAsync(role);
                }
            }

            var users = GetDefaultUsers();
            await _userManager.CreateAsync(users[0], "Admin1234");
            await _userManager.AddToRoleAsync(users[0], "ADMIN");
            await _userManager.CreateAsync(users[1], "User1234");
            await _userManager.AddToRoleAsync(users[1], "USER");
            await _userManager.CreateAsync(users[2], "User21234");
            await _userManager.AddToRoleAsync(users[2], "USER");
            await _context.SaveChangesAsync();
        }


    }
}