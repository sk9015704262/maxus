using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Users
{
    public class GetUserReqest
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
    }
}
