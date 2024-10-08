using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Topic
{
    public class TopicListResponse
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }

        public int IndustrySegmentId { get; set; }
        public string IndustrySegmentName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }
    }
}
