using PicturePilot.Data.Entities;

namespace PicturePilot.Data.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync();

    Task UpdateAsync();

    Task DeleteAsync();

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);
}
