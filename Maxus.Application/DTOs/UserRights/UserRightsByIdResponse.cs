using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.UserRights
{
    public class UserRightsByIdResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string UserName { get; set; }

        public int RightTypeId { get; set; }

        public string Rights { get; set; }

        public string AccessFor { get; set; }
        public List<tbl_UserRightsDetails> Details { get; set; }


    }
}
