using System.ComponentModel.DataAnnotations.Schema;

namespace PicturePilot.Data.Entities;

public class Image : BaseEntity
{
    public string Url { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User User { get; set; }

    public string Title { get; set; }

    public bool IsBLocked { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual HashSet<Tag> Tags { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}
