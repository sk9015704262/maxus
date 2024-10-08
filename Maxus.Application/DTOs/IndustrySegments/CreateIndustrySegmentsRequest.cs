using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.IndustrySegments
{
    public class CreateIndustrySegmentsRequest
    {
       // [Required(ErrorMessage = "kindly Enter Valid Industry Segment Code")]
        public string Code { get; set; }

       //  [Required(ErrorMessage = "kindly Enter Valid Industry Segment Code")]
        public string Name { get; set; }
       
    }
}
