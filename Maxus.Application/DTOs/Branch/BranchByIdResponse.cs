using Maxus.Application.DTOs.Common;

namespace Maxus.Application.DTOs.Branch
{
    public class BranchByIdResponse: IAuditable
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
