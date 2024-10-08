using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.AttachmentLimits
{
    public class AttachmentDto
    {
        public string AttachmentPath { get; set; }

        public string CreatedAt { get; set; } // Formatted date as string

        public string CreatedByName { get; set; }
    }
}
