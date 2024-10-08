using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.MOM
{
    public class GetMOMByIdResponse
    {
        public long Id { get; set; }

        public string Date { get; set; }
        public int SiteId { get; set; }
        public string SiteCode { get; set; }
        public string SiteName { get; set; }

        public string ActionBy { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public string CloserDate { get; set; }

        public List<tbl_ClientRepresentative> clientRepresentatives { get; set; }

        public List<tbl_ClientRepresentative> CompanyRepresentatives { get; set; }

        public List<tbl_MOMPoints> Points { get; set; }
        public string ActionablePoint { get; set; }

        public List<AttachmentDto> AttchmentPath { get; set; }

        public Boolean IsDraft { get; set; }

        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }




    }
}
