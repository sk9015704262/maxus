using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Interfaces
{
    public interface IVisitReportRepository
    {
        Task<tbl_VisitReport> CreateAsync(tbl_VisitReport obj);
        Task<tbl_VisitReport?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(tbl_VisitReport obj);
        Task<(FilterRecordsResponse, IEnumerable<tbl_VisitReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId, DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn);
    }
}
