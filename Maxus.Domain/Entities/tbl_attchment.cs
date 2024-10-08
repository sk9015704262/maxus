using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_attchment
    {
        public string AttachmentPath { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByName { get; set; }
    }
}
