using Maxus.Domain.Entities.PartialEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_VisitReport
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public long SiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }


        public string SiteSupervisorName { get; set; }

        public string Remarks { get; set; }

        public string Status { get; set; }


        public List<tbl_VisitChekList> VisitCheckList { get; set; }

        public List<string> Attachment { get; set; }

        public List<tbl_attchment> AttchmentPath { get; set; }

        public List<tbl_OptionVisitReport> VisitReportChecklist { get; set; }

        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }

       

        public long CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long UpdatedBy { get; set; }

        public Boolean IsDraft { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }
    }
}
