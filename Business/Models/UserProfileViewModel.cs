using PicturePilot.Data.Entities;

namespace PicturePilot.Business.Models;

public class UserProfileViewModel
{
    public User User { get; set; }
    public IEnumerable<Comment> LastComments { get; set; }
    public IEnumerable<Image> Images { get; set; }
}
