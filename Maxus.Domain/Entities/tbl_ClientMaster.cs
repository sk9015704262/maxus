using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_ClientMaster
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }

    }
}
