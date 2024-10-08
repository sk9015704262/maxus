using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.CustomerFeedbackReport
{
    public class CustomerFeedbackMasterResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<tbl_OptionFeedbackReport> Options { get; set; }
    }
}
