using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReportChekList
{
    public class VisitReportByCompanyListResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long IndustrySegmentId { get; set; }

        public string IndustrySegmentName { get; set; }

        public Boolean IsMandatory { get; set; }
        
        public List<tbl_CheklistOption> cheklistOptions { get; set; }

        public long CompanyId { get; set; }

        public string CreatedAt { get; set; }

        public object CreatedBy { get; set; }

        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }
    }
}
