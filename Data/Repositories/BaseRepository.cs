using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Interfaces;

namespace PicturePilot.Data.Repositories;

public class BaseRepository<T>(PicturesDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly PicturesDbContext _context = context;

    protected readonly DbSet<T> _entities = context.Set<T>();

    public async Task<int> AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        _entities.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        _context.SaveChanges();
    }
}
