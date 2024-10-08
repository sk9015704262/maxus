namespace Maxus.Application.DTOs.CustomerFeedbackOption
{
    public class CreateCustomerFeedbackOptionRequest
    {
        public string Name { get; set; }
        public Boolean IsDefault { get; set; }

        public long CompanyId { get; set; }
    }
}
