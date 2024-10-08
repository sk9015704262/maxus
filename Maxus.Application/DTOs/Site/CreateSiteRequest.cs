using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Site
{
    public class CreateSiteRequest
    {
        [Required]
        public int BranchId { get; set; }

        [Required]
        public int ClidetId { get; set; }

        [Required]
        public string Code { get; set; }
        
        [Required]

        public string Name { get; set; }

        [Required]

        public string Address { get; set; }

        [Required]

        public decimal Latitude { get; set; }

        [Required]

        public decimal Longitude { get; set; }

        [Required]

        public int IndustrySegmentId { get; set; }

        public List<tbl_ClientRepresentativeDetails> ClientRepresentatives { get; set; }
    }
}
