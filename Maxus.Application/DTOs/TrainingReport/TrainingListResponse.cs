using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.TrainingReport
{
    public class TrainingListResponse
    {
        public int Id { get; set; }
        public long SiteId { get; set; }
        public string SiteCode { get; set; }
        public string Date { get; set; }

        public string SiteName { get; set; }
        public int Duration { get; set; }

        public string TrainerName { get; set; }

        public string TrainerDesignation { get; set; }

        public string Department { get; set; }

        public string TrainerFeedback { get; set; }

        public string Status { get; set; }

        public string ActionPlan { get; set; }


        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
        public Boolean IsDraft { get; set; }
    }
}
