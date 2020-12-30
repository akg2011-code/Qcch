using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Interfaces
{
    public interface ICreationAuditable
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
