using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QccHub.Data.Models
{
    public class JobPosition : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public int? YearsOfExperience { get; set; }
        public virtual ICollection<UserJobPosition> EmployeeJobs { get; } = new List<UserJobPosition>();
    }
}
