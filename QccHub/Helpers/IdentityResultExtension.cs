using Microsoft.AspNetCore.Identity;
using QccHub.Logic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Helpers
{
    public static class IdentityResultExtensions
    {
        public static string GetErrors(this IdentityResult result)
        {
            string errorString = "";
            foreach (var error in result.Errors)
            {
                if (error.Code == "InvalidToken")
                {
                    errorString = errorString.AddError("Token", error.Description);
                }
                else if (error.Code != "DuplicateUserName")
                    errorString = errorString.AddError(error.Code.Replace("Duplicate", ""), error.Description);
            }
            return errorString;
        }
    }
}
