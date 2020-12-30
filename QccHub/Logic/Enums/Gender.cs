using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Logic.Enums
{
    public enum Gender
    {
        [Display(Name = "Not Specified")]
        Other = 0,
        Male,
        Female
    }
}
