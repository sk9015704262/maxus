﻿using Maxus.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.VisitReport
{
    public class VisitReportListRequest : PaginationFilter
    {
        public int CompanyId { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public Boolean? IsDraft { get; set; }

    }
}
