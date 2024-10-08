using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Domain.Entities
{
    public class tbl_Users 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Designation { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public int RoleId { get; set; }

        public string UserType { get; set; }

        public string CompanyId { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
    }
}
