using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_VisitChekList
    {
       

        public long Id { get; set; }

        public Boolean IsSelected { get; set; }

        public string ActionTaken { get; set; }

        public DateTime ClosureDate { get; set; }
    }
}
