using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Models
{
    public class JobCategory : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
