using Maxus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Users
{
    public class UsersListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string UserType { get; set; }

        public string CreatedAt { get; set; }

        public object CreatedBy { get; set; }
        public string UpdatedAt { get; set; }

        public object UpdatedBy { get; set; }

    }
}
