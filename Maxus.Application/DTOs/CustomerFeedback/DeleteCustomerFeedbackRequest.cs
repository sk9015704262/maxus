using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.CustomerFeedback
{
    public class DeleteCustomerFeedbackRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
