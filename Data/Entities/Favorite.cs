using System.ComponentModel.DataAnnotations.Schema;

namespace PicturePilot.Data.Entities;

public class Favorite
{
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User User { get; set; }

    [ForeignKey(nameof(Image))]
    public int ImageId { get; set; }

    public Image Image { get; set; }
}
