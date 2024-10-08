using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.DTOs
{
    public class FilterRecordsResponse
    {
        public int TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
    }
}
