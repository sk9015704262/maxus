using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.MOM
{
    public class UpdateMOMReportRequest
    {
        public int Id { get; set; }
        public long SiteId { get; set; }

        public string ActionBy { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public DateTime CloserDate { get; set; }

        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public List<tbl_ClientRepresentative> CompanyRepresentatives { get; set; }

        public List<tbl_MOMPoints> Points { get; set; }

        public string ActionablePoint { get; set; }

        public List<string> Attachment { get; set; }

        public Boolean IsDraft { get; set; }
    }
}
