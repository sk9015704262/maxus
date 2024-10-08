using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities.PartialEntities
{
    public class tbl_OptionVisitReport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string OptionName { get; set; }

        public string ActionTaken { get; set; }

        public DateTime ClosureDate { get; set; }




    }
}
