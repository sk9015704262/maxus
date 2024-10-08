using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface IMOMReportService
    {
        Task<int> CreateAsync(CreateMOMRequest obj);

        Task<(FilterRecordsResponse, IEnumerable<MOMReportListResponse>)> GetAllAsync(GetMOMReportRequest obj);

        Task<IEnumerable<GetMOMByIdResponse>?> GetMOMReportByCompanyAsync(GetMOMReportByCompanyIdRequest request);

        Task<GetMOMByIdResponse?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, CreateMOMRequest request);
    }
}
