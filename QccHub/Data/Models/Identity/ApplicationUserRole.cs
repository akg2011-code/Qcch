using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Models
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationRole Role { get; set; }

        public ApplicationUserRole() { }
        public ApplicationUserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
