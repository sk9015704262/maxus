namespace Maxus.Application.DTOs.CustomerFeedbackOption
{
    public class CustomerFeedbackOptionListResponse
    {
        public long id { get; set; }
        public string Name { get; set; }
        public Boolean IsDefault { get; set; }

        public long CompanyId { get; set; }

        public string CreatedAt { get; set; }

        public object CreatedBy { get; set; }

        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }
    }
}
