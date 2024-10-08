using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Company
{
    public class DeleteCompanyRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
