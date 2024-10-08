using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class SiteByIdResponse
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
        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
        public List<tbl_ClientRepresentativeDetails> ClientRepresentatives { get; set; }
    }
}
