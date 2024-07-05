using Microsoft.AspNetCore.Identity;

namespace PicturePilot.Data.Entities;

public class User : IdentityUser<int>
{
    public string Login { get; set; }
}
