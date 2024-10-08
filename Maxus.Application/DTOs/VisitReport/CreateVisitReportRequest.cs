using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class CreateVisitReportRequest
    {
        public int Id { get; set; }
        

        public long SiteId { get; set; }

        public string SiteSupervisorName { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }

        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }

        public List<tbl_VisitChekList> VisitCheckList { get; set; }

        public List<string> Attachment { get; set; }

        public DateTime CreatedAt { get; set; }

        public Boolean IsDraft { get; set; }

    }
}
