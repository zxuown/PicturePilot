using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<int> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);
}
