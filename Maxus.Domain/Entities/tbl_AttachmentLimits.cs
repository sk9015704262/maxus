using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_AttachmentLimits
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ClientId { get; set; }

        public long SiteId { get; set; }

        public long AttachmentTypeId { get; set; }

        public string CompanyName { get; set; }
        public string ClientName { get; set; }
        public string SiteName { get; set; }

        public List<tbl_AttachmentTypeId> Ids { get; set; }

        public object? AttachmentType { get; set; }

        public string Right { get; set; }

        public Boolean Compulsion { get; set; }

        public int LimitCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long CreatedBy { get; set; }

        public long UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }
    }
}
