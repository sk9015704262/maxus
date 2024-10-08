using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class GetVisitReportRequestChekList
    {
        [Required]
        public int Id { get; set; }
    }
}
