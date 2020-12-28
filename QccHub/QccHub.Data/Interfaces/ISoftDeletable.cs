using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
