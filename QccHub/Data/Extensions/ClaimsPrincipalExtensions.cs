using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace QccHub.Data.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) =>
            principal.FindFirstValue(ClaimTypes.PrimarySid);

        public static string GetUserName(this ClaimsPrincipal principal) =>
           principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
