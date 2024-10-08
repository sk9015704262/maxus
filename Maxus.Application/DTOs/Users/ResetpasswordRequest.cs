using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Users
{
    public class ResetpasswordRequest
    {
        public long UserId { get; set; }

        public string password { get; set; }
    }
}
