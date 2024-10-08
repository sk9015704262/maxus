using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.dashboard
{
    public class DashBoardListRequest
    {
        public int CompanyId { get; set; }

        public Boolean? IsDraft { get; set; }

        public string Date { get; set; }
    }
}
