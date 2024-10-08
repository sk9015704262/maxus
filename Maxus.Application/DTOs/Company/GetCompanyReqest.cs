using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Company
{
    public class GetCompanyRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
