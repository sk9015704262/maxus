using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Mail
{
    public class SendReportMailRequest
    {
        

        public int ReportType { get; set; }
        public int Id { get; set; }
    }
}
