﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.MOM
{
    public class GetMOMReportByCompanyIdRequest
    {
        public int UserId { get; set; }

        public int CompanyId { get; set; }
    }
}
