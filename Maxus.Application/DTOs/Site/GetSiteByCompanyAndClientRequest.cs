using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class GetSiteByCompanyAndClientRequest
    {
        public int CompanyId { get; set; }
        public long ClientId { get; set; }
    }
}
