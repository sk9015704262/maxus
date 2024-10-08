using System.ComponentModel.DataAnnotations;

namespace AccountingAPI.Application.DTOs
{
    public class VerifyDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

       
    }
}
