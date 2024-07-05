using PicturePilot.Data.Entities;
using PicturePilot.Data.Interfaces;

namespace PicturePilot.Data.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    public Task AddAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync()
    {
        throw new NotImplementedException();
    }
}
