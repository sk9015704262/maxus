using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.Company
{
    public class CreateCompanyRequest
    {
       // [Required(ErrorMessage = "kindly Enter Valid Company Code")]
        public string Code { get; set; }

       // [Required(ErrorMessage = "kindly Enter Valid Company Name")]
        public string Name { get; set; }
       
    }
}
