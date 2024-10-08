using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tblTrainingReport
    {
        public int Id { get; set; }
        public long SiteId { get; set; }

        public DateTime Date { get; set; }

        public string SiteCode { get; set; }
        public string SiteName { get; set; }

        public int Duration { get; set; }

        public string TrainerName { get; set; }

        public string TrainerDesignation { get; set; }

        public string Department { get; set; }

        public string TrainerFeedback { get; set; }

        public string Status { get; set; }

        public string ActionPlan { get; set; }

        public List<TrainingReportAttendance> TrainingReportAttendance { get; set; }

        public List<TrainingReportTopics> TopicId { get; set; }

        public List<tbl_CreateTopicTraningReportRequest> Topic { get; set; }

        public List<tbl_ClientRepresentative> ClientRepresentative { get; set; }

        public string TrainerSignature { get; set; }

        public string EmployeeSignature { get; set; }

        public string AdditionalTrainerSignature { get; set; }

        public string ClientSignature { get; set; }

        public DateTime CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }
        public Boolean IsDraft { get; set; }

        public List<string> Attachment { get; set; }

        public List<tbl_attchment> AttchmentPath { get; set; }

    }
}
