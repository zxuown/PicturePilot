namespace PicturePilot.Business.Models;

public class SearchModel
{
    public string? Query { get; set; }
    public ICollection<string> Tags { get; set; }
}
