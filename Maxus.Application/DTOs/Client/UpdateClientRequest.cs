using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Client
{
    public class UpdateClientRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
