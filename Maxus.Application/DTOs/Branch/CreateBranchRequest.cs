using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Branch
{
    public class CreateBranchRequest
    {
        //[StringLength(10, MinimumLength = 2, ErrorMessage = "Branch Code must be between 2 and 10 characters.")]
        //[Required(ErrorMessage = "kindly Enter Valid Branch Code")]
        public string Code { get; set; }

        //[MaxLength(200)]
        //[Required(ErrorMessage = "kindly Enter Valid Branch Name")]
        public string Name { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "The Company Id must be greater than 0.")]
        public int CompanyId { get; set; }

    }
}
