using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class TagRepository(PicturesDbContext context): BaseRepository<Tag>(context)
{

}
