using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.UserRights
{
    public class UserRightListResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Rights { get; set; }

        public int RightTypeId { get; set; }

        public string CreatedAt { get; set; }


        public string AccessFor { get; set; }

        public object CreatedBy { get; set; }

        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }

      
    }
}
