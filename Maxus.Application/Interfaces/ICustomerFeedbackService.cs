using Maxus.Application.DTOs.CustomerFeedback;
using Maxus.Application.DTOs.CustomerFeedbackOption;

namespace Maxus.Application.Interfaces
{
    public interface ICustomerFeedbackService :  IBaseService<CustomerFeedbackListRequest, CustomerFeedbackListResponse, CustomerFeedbackByIdResponse, CreateCustomerFeedbackRequest, UpdateCustomerFeedbackRequest>
    {
        Task<IEnumerable<CustomerFeedbackByCompanyListResponse>> GetCustomerFeedbackByCompanyIdAsync(GetCustomerFeedbackByCompanyIdRequest obj , int UserId);
    }
}
