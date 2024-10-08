using Maxus.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Client
{
    public class ClientListRequest : PaginationFilter
    {
        [Required]
        public long CompanyId { get; set; }
    }
}
