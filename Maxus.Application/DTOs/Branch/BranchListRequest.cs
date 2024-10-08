using Maxus.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Branch
{
    public class BranchListRequest : PaginationFilter
    {
        [Required]
        public long CompanyId { get; set; }
    }
}
