using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IMOMReportRepository
    {
        Task<tbl_MOMReport> CreateAsync(tbl_MOMReport obj);

        Task<(FilterRecordsResponse, IEnumerable<tbl_MOMReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int CompanyId, DateTime? FromDate , DateTime? ToDate , Boolean? IsDraft , int? SearchColumn);

        Task<IEnumerable<tbl_MOMReport>> GetByCompanyIdAsync(int UserId, int Comapnyid);

        Task<tbl_MOMReport?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(tbl_MOMReport obj);
    }
}
