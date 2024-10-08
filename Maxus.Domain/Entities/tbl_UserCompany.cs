using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_UserCompany
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public DateTime CreatedAt { get; set; }

        public long CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long UpdatedBy { get; set; }
    }
}
