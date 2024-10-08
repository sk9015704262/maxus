using Maxus.Application.DTOs.CustomerFeedbackReport;
using Maxus.Application.DTOs.MOM;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface ICustomerFeedbackReportService
    {
        Task<int> CreateCustomerFeedbackReport(CreateCustomerFeedbackReportRequest request);

        Task<IEnumerable<CustomerFeedbackReportListResponse>?> GetFeedbackByCompanyIdAsync(GetCustomerFeedbackReportByCompanyRequest request);

        Task<CustomerFeedbackReportByIdResponse?> GetByIdAsync(int id);

       
        Task<bool> UpdateAsync(int id, CreateCustomerFeedbackReportRequest request);

        Task<(FilterRecordsResponse, IEnumerable<CustomerFeedbackReportListResponse>)> GetAllAsync(CustomerFeedbackReportListRequest obj);
    }
}
