namespace PicturePilot.Business.Models;

public class ImageUploadViewModel
{
    public string Title { get; set; }
    public IFormFile ImageFile { get; set; }
}
