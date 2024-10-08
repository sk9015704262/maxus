using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Designation { get; set; }

        public string Username { get; set; }

        public string CompanyId { get; set; }

        public int RoleId { get; set; }
    }
}
