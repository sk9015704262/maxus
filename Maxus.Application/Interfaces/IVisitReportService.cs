using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.Interfaces
{
    public interface IVisitReportService
    {
        Task<int> CreateAsync(CreateVisitReportRequest obj);
        Task<VisitReportByidResponse?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, CreateVisitReportRequest request);
        Task<(FilterRecordsResponse, IEnumerable<VisitReportByListResponse>)> GetAllAsync(VisitReportListRequest obj);
    }
}
