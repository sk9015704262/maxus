using System.ComponentModel.DataAnnotations;

namespace Maxus.Application.DTOs.CustomerFeedback
{
    public class CreateCustomerFeedbackRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public long IndustrySegmentId { get; set; }
        [Required]

        public Boolean IsMandatory { get; set; }
        [Required]

        public long CompanyId { get; set; }

        public string ChekListOption { get; set; }
    }
}
