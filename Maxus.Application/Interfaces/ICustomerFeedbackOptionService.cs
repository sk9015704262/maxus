using Maxus.Application.DTOs.CustomerFeedbackOption;

namespace Maxus.Application.Interfaces
{
    public interface ICustomerFeedbackOptionService : IBaseService<CustomerFeedbackOptionListRequest, CustomerFeedbackOptionListResponse, CustomerFeedbackOptionByIdResponse, CreateCustomerFeedbackOptionRequest, UpdateCustomerFeedbackOptionRequest>
    {

    }
}
