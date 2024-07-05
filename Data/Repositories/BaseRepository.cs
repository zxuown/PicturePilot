﻿using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Interfaces;

namespace PicturePilot.Data.Repositories;

public class BaseRepository<T>(PicturesDbContext context) : IRepository<T> where T : BaseEntity
{
    private readonly PicturesDbContext _context = context;

    private readonly DbSet<T> _entities = context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        _entities.Remove(item);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return _entities.AsNoTracking().ToList();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
    }
}
