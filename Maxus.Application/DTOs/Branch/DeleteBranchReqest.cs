using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Branch
{
    public class DeleteBranchRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public int Id { get; set; }
    }
}
