using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Repositories;

public class ReportRepository(PicturesDbContext context) : BaseRepository<Report>(context)
{
    public async new Task<IEnumerable<Report>> GetAllAsync()
    {
        return await _entities.Include(x => x.Sender).AsNoTracking().ToListAsync();
    }
}
