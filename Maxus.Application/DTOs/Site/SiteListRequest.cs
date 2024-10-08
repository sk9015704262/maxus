using Maxus.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class SiteListRequest : PaginationFilter
    {
        public int CompanyId { get;set; }
    }
}
