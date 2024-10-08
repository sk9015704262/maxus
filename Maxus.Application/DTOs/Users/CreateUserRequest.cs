using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Application.DTOs.Users
{
    public class CreateUserRequest
    {
       // [Required(ErrorMessage = "Full Name is required.")]
      //  [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Kindly enter alphabets only in Full Name.")]

        public string Name { get; set; }


       // [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit contact number.")]
        public string PhoneNo { get; set; }


       // [EmailAddress(ErrorMessage = "Kindly enter a valid email ID.")]
        public string Email { get; set; }

       

        public string Designation { get; set; }


       // [RegularExpression(@"^\S*$", ErrorMessage = "Kindly remove space from User Name.")]
        public string Username { get; set; }

       // [MinLength(8, ErrorMessage = "Kindly enter a password of at least 8 characters.")]
       // [RegularExpression(@"^(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$", ErrorMessage = "Kindly include at least one special character in the password.")]
        public string Password { get; set; }


        public int RoleId { get; set; }
        public string CompanyId { get; set; }

    }
}
