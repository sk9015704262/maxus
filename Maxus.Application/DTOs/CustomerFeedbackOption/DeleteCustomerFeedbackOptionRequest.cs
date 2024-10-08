using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.CustomerFeedbackOption
{
    public class DeleteCustomerFeedbackOptionRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
