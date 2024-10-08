using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.UserFromRight
{
    public class GetUserFormRightRequest
    {
        public int userId { get; set; }

        public int FormId { get; set; }
    }
}
