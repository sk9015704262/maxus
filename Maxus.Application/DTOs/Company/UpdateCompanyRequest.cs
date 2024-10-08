using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Company
{
    public class UpdateCompanyRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
