using Maxus.Domain.Entities;

namespace Maxus.Application.DTOs.AttachmentLimits
{
    public class CreateAttachmentLimitRequest
    {
       

        public int? AttachmentType { get; set; }

        public long ReportTypeId { get; set; }

        public Boolean Compulsion { get; set; }

        public int LimitCount { get; set; }

        public List<tbl_AttachmentTypeId> Id { get; set; }

    }
}
