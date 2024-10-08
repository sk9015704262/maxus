using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.MOM
{
    public class MOMReportListResponse
    {
        public long Id { get; set; }

        public string Date { get; set; }
        public long SiteId { get; set; }

        public string SiteCode { get; set; }

        public string SiteName { get; set; }

        public string ActionBy { get; set; }

        public string Status { get; set; }

        public string ActionablePoint { get; set; }


        public string Remark { get; set; }

        public string CloserDate { get; set; }

        public Boolean IsDraft { get; set; }

        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
    }
}
