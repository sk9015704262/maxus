using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.UserRights
{
    public class UpdateUserRightRequest
    {
      
        public long Id { get; set; }

        public int RightTypeId { get; set; }

        public List<tbl_UserRightsDetails> Details { get; set; }
    }
}
