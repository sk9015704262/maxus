using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class VisitReportByIdResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long IndustrySegmentId { get; set; }

        public Boolean IsMandatory { get; set; }
       

        public long CompanyId { get; set; }

        public string CreatedAt { get; set; }

        public object CreatedBy { get; set; }

        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }

        public List<tbl_CustomerCheklistOption> CustomerCheklistOption { get; set; }

    }
}
