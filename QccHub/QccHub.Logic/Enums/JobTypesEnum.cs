using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QccHub.Logic.Enums
{
    public enum JobTypesEnum
    {
        [Display(Name = "Full Time")]
        FullTime = 1,
        [Display(Name = "Part Time")]
        PartTime
    }
}
