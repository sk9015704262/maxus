using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_MOMReport
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public long SiteId { get; set; }

        public string SiteCode { get; set; }
        public string SiteName { get; set; }

        public string ActionBy { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public DateTime CloserDate { get; set; }

        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public List<tbl_ClientRepresentative> CompanyRepresentatives { get; set; }

        public List<tbl_MOMPoints> Points { get; set; }

        public List<tbl_attchment> AttchmentPath { get; set; }

        public string ActionablePoint { get; set; }

        public Boolean IsDraft { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }

        public List<string> Attachment { get; set; }

       



    }
}
