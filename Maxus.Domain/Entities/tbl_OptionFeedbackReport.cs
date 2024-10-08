using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_OptionFeedbackReport
    {
        public int Id { get; set; }

        public string OptionName { get; set; }
        public int CustomerFeedbackId { get; set; }
    }
}
