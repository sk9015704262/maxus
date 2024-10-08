using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Topic
{
    public class UpdateTopicRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int IndustrySegmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
