using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.CustomerFeedbackReport
{
    public class CustomerFeedbackReportUpdateRequest
    {
        public int Id { get; set; }
        public long SiteId { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }

        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }
        public List<tbl_CheklistReport> FeedbackCheckList { get; set; }

        public List<string> Attachment { get; set; }

        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public Boolean IsDraft { get; set; }
    }
}
