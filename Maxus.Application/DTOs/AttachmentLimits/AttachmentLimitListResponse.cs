using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.AttachmentLimits
{
    public class AttachmentLimitListResponse
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ClientId { get; set; }

        public long SiteId { get; set; }

        public long AttachmentTypeId { get; set; }

        public string AttachmentType { get; set; }

        public string Right { get; set; }

        public Boolean Compulsion { get; set; }

        public int LimitCount { get; set; }

        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public Object CreatedBy { get; set; }

        public Object UpdatedBy { get; set; }
    }
}
