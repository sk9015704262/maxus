using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface ITraningReportRepository 
    {
        Task<tblTrainingReport> CreateAsync(tblTrainingReport obj);

        Task<bool> UpdateAsync(tblTrainingReport obj);
        Task<tblTrainingReport?> GetByIdAsync(int id);


        Task<(FilterRecordsResponse, IEnumerable<tblTrainingReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId, DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn);
    }
}
