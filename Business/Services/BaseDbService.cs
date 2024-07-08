using Microsoft.EntityFrameworkCore;
using PicturePilot.Data;

namespace PicturePilot.Business.Services
{
    public abstract class BaseDbService<TEntity>(PicturesDbContext context) where TEntity : class
    {
        protected readonly DbSet<TEntity> dbSet = context.Set<TEntity>();
        protected readonly PicturesDbContext context = context;
    }
}
