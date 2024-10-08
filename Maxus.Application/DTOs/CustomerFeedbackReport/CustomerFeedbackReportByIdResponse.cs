using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Domain.Entities;
using Maxus.Domain.Entities.PartialEntities;

namespace Maxus.Application.DTOs.CustomerFeedbackReport
{
    public class CustomerFeedbackReportByIdResponse
    {
        public long Id { get; set; }

        public int SiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string Remark { get; set; }

        public string Status { get; set; }


        public string Date { get; set; }


        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public List<AttachmentDto> AttchmentPath { get; set; }
        

        public string ClientSignature { get; set; }

        public string ManagerSignature { get; set; }

        public Boolean IsDraft { get; set; }


        public string UpdatedAt { get; set; }

        public object CreatedBy { get; set; }

        public object UpdatedBy { get; set; }
        public string CreatedAt { get; set; }
        public List<CustomerFeedbackMasterResponseDTO> CheckLists { get; set; }
    }
}
