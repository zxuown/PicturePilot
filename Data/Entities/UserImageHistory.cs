using PicturePilot.Data.Entities;

public class UserImageHistory
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int ImageId { get; set; }
    public Image Image { get; set; }

    public DateTime ViewedAt { get; set; } 
}