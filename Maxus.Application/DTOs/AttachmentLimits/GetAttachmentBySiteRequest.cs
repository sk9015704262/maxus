using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.AttachmentLimits
{
    public class GetAttachmentBySiteRequest
    {
        public int SiteId { get; set; }

        public int ReportType { get; set; }
    }
}
