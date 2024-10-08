using Maxus.Domain.DTOs;

namespace Maxus.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<(FilterRecordsResponse, IEnumerable<T>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn);
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T obj);
        Task<bool> UpdateAsync(T obj);
        Task<bool> DeleteAsync(int id);
    }
}
