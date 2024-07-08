using Microsoft.AspNetCore.Identity;

namespace PicturePilot.Data.Entities;

public class User : IdentityUser<int>
{
    public string Login { get; set; }

    public string Name { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }
}
