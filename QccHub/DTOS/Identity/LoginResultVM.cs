using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class LoginResultVM
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
    }
}
