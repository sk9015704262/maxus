using Maxus.Domain.Entities.PartialEntities;

namespace Maxus.Domain.Entities
{
    public class tbl_CustomerFeedbackReport
    {
        public long Id { get; set; }

        public long SiteId { get; set; }

        public string Remark { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }

        public string SiteCode { get; set; }
        public string SiteName { get; set; }

        public List<tbl_CheklistReport> FeedbackCheckList { get; set; }
        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public List<string> Attachment { get; set; }

        public List<tbl_attchment> AttchmentPath { get; set; }

        public List<tbl_OptionFeedbackReport> Option { get; set; }

        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }


        public Boolean IsDraft { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }
        public List<tbl_CustomerFeedbackMaster> CheckLists { get; set; }
        public List<tbl_OptionFeedbackReport> CheckListOptions { get; set; }

    }
}
