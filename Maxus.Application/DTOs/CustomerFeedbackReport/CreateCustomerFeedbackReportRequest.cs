using Maxus.Domain.Entities;

namespace Maxus.Application.DTOs.CustomerFeedbackReport
{
    public class CreateCustomerFeedbackReportRequest
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

        public DateTime CreatedAt { get; set; }

        public Boolean IsDraft { get; set; }
    }
}
