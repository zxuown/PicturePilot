namespace PicturePilot.Data.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public User User { get; set; }

    public Image Image { get; set; }

    public DateTime CreatedAt { get; set; }
}
