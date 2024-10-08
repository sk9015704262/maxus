using Maxus.Application.DTOs.TrainingReport;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface ITrainingReportService
    {
        Task<int> CreateAsync(CreateTrainingReportRequest request );

        Task<bool> UpdateAsync(int id, CreateTrainingReportRequest request);

        Task<(FilterRecordsResponse, IEnumerable<TrainingListResponse>)> GetAllAsync(TrainingReportListRequest request);

        Task<TrainingReportByIdResponse?> GetByIdAsync(int id);
    }
}
