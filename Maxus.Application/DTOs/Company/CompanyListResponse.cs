namespace Maxus.Application.DTOs.Company
{
    public class CompanyListResponse
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
    }
}
