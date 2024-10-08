using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class GetSiteByUserRequest
    {
        public int UserId { get; set; }

        public int ComapanyId { get; set; }
    }
}
