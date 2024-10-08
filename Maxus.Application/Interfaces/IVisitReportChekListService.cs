using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.DTOs.VisitReportChekList;

namespace Maxus.Application.Interfaces
{
    public interface IVisitReportChekListService : IBaseService<VisitReportChekListRequest, VisitReportListResponse, VisitReportByIdResponse, CreateVisitReportCheckListRequest, UpdateVisitReportChekListRequest>
    {
        Task<IEnumerable<VisitReportByCompanyListResponse>?> GetVisitByCompanyIdAsync(GetVisitByCompanyRequest request , int UserId);
    }
}
