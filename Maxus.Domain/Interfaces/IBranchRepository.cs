using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IBranchRepository : IBaseRepository<tbl_BranchMaster>
    {
        Task<(FilterRecordsResponse, IEnumerable<tbl_BranchMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , long CompanyId , int? SearchColumn);
    }
}
