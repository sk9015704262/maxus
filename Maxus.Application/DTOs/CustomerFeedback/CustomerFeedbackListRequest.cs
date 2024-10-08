using Maxus.Application.Common;

namespace Maxus.Application.DTOs.CustomerFeedback
{
    public class CustomerFeedbackListRequest : PaginationFilter
    {
        public int CompanyId { get;set; }
    }
}
