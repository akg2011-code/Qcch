using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QccHub.Data.Interfaces;

namespace QccHub.Data.Models
{
    public class Project : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get ; set ; }
        public int CreatedBy { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public virtual ApplicationUser Company { get; set; }
    }
}
