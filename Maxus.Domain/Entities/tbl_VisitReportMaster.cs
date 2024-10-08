using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_VisitReportChekListMaster
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long IndustrySegmentId { get; set; }

        public string IndustrySegmentName { get; set; }


        public Boolean IsMandatory { get; set; }

        public string ChekListOption { get; set; }

        public int CheckListId { get; set; }

        public string ChekListName { get; set; }

        public List<tbl_CustomerCheklistOption> CustomerCheklistOption { get; set; }

        public List<tbl_CheklistOption> cheklistOptions { get; set; }


        public long CompanyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public object CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }

        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }

    }
}
