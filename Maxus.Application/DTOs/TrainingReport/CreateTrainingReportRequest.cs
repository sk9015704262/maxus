using Maxus.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.TrainingReport
{
    public class CreateTrainingReportRequest
    {

        public int Id { get; set; }
        [Required]

        public long SiteId { get; set; }

      

        [Required]
        public int Duration { get; set; }

        [Required]

        public string TrainerName { get; set; }

        [Required]
        public string TrainerDesignation { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]

        public string TrainerFeedback { get; set; }

        [Required]

        public string Status { get; set; }

        public string TrainerSignature { get; set; }

        public string EmployeeSignature { get; set; }

        public string AdditionalTrainerSignature { get; set; }

        public string ClientSignature { get; set; }

        [Required]

        public string ActionPlan { get; set; }

        [Required]

        public List<TrainingReportAttendance> TrainingReportAttendance { get; set; }

        [Required]      
        public List<tbl_CreateTopicTraningReportRequest> Topic { get; set; }

        [Required]
        public List<tbl_ClientRepresentative> ClientRepresentative { get; set; }

        public List<string> Attachment { get; set; }

        public Boolean IsDraft { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
