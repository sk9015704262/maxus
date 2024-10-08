using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_TopicsMaster
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string CompanyCode { get; set; }
        public int IndustrySegmentId { get; set; }
        public string IndustrySegmentName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public object CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object UpdatedBy { get; set; }

        public string UpdatedByName { get; set; }

        public string CreatedByName { get; set; }


    }
}
