using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.TrainingReport
{
    public class GetTraningReportByCompanyIdRequest
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }
    }
}
