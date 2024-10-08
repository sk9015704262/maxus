using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.CustomerFeedback
{
    public class CustomerFeedbackByCompanyListResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long IndustrySegmentId { get; set; }

        public string IndustrySegmentName { get; set; }


        public Boolean IsMandatory { get; set; }

        public long CompanyId { get; set; }

        public List<tbl_CheklistOption?> cheklistOptions { get; set; }


        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
