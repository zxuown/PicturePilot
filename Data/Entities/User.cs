using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PicturePilot.Data.Entities;

public class User : IdentityUser<int>
{
    public string Login { get; set; }

    public string Name { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsBLocked { get; set; }

    [InverseProperty(nameof(Image.User))]
    public virtual ICollection<Image> Images { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; }

    public virtual HashSet<UserImageHistory> History { get; set; }
}
