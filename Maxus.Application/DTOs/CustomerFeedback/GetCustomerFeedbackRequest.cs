using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.CustomerFeedback
{
    public class GetCustomerFeedbackRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
