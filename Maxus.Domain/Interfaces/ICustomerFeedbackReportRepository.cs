using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface ICustomerFeedbackReportRepository
    {
        Task<tbl_CustomerFeedbackReport> CreateAsync(tbl_CustomerFeedbackReport obj);

        Task<IEnumerable<tbl_CustomerFeedbackReport?>> GetFeedbackByComanyIdAsync(int userId, int companyId);

        Task<tbl_CustomerFeedbackReport?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(tbl_CustomerFeedbackReport obj);

            
        Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerFeedbackReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId , DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn);

    }
}
