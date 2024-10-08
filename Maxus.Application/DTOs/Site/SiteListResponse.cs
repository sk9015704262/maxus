using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class SiteListResponse
    {
        public long Id { get; set; }
      

        public string BranchCode { get; set; }

     
        public string ClientCode { get; set; }


        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }


      
        public string IndustrySegmentName { get; set; }

        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
    }
}
