using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Domain.Entities;
using Maxus.Domain.Entities.PartialEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class VisitReportByidResponse
    {
        public long Id { get; set; }

        public string Date { get; set; }
        public int SiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }

        public string SiteSupervisorName { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }


        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }

        public List<AttachmentDto> AttchmentPath { get; set; }
        

        public object CreatedBy { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }

        public List<tbl_OptionVisitReport> VisitReportChecklist { get; set; }
    }
}
