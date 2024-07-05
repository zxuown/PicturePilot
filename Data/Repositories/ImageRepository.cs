using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class ImageRepository(PicturesDbContext context)  : BaseRepository<Image>(context)
{
}
