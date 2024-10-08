using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.CustomerFeedbackOption
{
    public class UpdateCustomerFeedbackOptionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsDefault { get; set; }

        public long CompanyId { get; set; }
    }
}
