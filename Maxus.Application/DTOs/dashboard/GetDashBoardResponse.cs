using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.dashboard
{
    public class GetDashBoardResponse
    {
        public int MomReports { get; set; }
        public int VisitReports { get; set; }
        public int CustomerFeedbackReports { get; set; }
        public int TrainingReports { get; set; }
    }
}
