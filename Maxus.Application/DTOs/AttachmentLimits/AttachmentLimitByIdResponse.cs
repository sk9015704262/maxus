namespace Maxus.Application.DTOs.AttachmentLimits
{
    public class AttachmentLimitByIdResponse
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ClientId { get; set; }

        public long SiteId { get; set; }
        public string CompanyName { get; set; }
        public string ClientName { get; set; }
        public string SiteName { get; set; }


        public long AttachmentTypeId { get; set; }

        public string Right { get; set; }

        public string AttachmentType { get; set; }

        public Boolean Compulsion { get; set; }

        public int LimitCount { get; set; }


        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
    }



}
