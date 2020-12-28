using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QccHub.DTOS
{
    public class UserRegisteration
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password),Compare("Password",ErrorMessage = "Passwords are not matched")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public string Position { get; set; }
        public int RoleId { get; set; }

    }
}
