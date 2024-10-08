using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class UpdateVisitReportChekListRequest
    {
        [Required]
        public int Id { get; set; }
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
