using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_SiteMaster
    {
        public long Id { get; set; }
        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public int ClidetId { get; set; }

        public string ClientName { get; set; }

        public string ClientCode { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }


        public int IndustrySegmentId { get; set; }

        public string IndustrySegmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }
        public List<tbl_ClientRepresentativeDetails> ClientRepresentatives { get; set; }
    }
}
