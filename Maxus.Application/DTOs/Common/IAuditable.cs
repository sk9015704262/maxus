using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Common
{
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        string CreatedAt { get; set; }
        string UpdatedAt { get; set; }
    }
}
