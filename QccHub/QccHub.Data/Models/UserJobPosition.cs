using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QccHub.Data.Models
{
    public class UserJobPosition : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public int CreatedBy { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCurrentPosition { get; set; }
        public bool IsDeleted { get ; set ; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int JobPositionId { get; set; }
        public virtual ApplicationUser Company { get; set; }
        public virtual ApplicationUser Employee { get; set; }
        public virtual JobPosition JobPosition { get; set; }
    }
}
