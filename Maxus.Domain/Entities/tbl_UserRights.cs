using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_UserRights
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Rights { get; set; }


        public int RightTypeId { get; set; }

        public List<tbl_UserRightsDetails> Details { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByName { get; set; }

        public long CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string AccessFor { get; set; }
    }
}
