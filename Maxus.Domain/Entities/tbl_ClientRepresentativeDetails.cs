using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_ClientRepresentativeDetails
    {
        public int RepresentativeId { get; set; }
        public string RepresentativeName { get; set; }
        public string Designation { get; set; }
        public string? Email { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string PhoneNo { get; set; }

    }
}
