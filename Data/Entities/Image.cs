namespace PicturePilot.Data.Entities;

public class Image : BaseEntity
{
    public string Url { get; set; }

    public User user { get; set; }

    public bool IsBLocked { get; set; }

    public List<Tag> Tags { get; set; }
}
