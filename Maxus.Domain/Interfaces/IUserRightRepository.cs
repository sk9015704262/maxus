using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IUserRightRepository
    {
        Task<tbl_UserRights> CreateAsync(tbl_UserRights obj);

        Task<(FilterRecordsResponse, IEnumerable<tbl_UserRights>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn);

        Task<tbl_UserRights?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(tbl_UserRights obj);

        Task<bool> DeleteAsync(int id);
    }
}
