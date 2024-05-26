using API.Models;

namespace API.Interfaces;

public interface IGenericRepository<T> where T: class
{
    public Task<T> GetByIdAsync(int id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> CreateAsync(T entity);
    public Task<T> DeleteAsync(int id);
}
