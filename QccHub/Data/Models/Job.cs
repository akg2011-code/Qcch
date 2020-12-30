using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Job : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public virtual ApplicationUser Company { get; set; }
        public string Location { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsB2B { get; set; }
        public decimal Salary { get; set; }
        public int Type { get; set; }

        //[ForeignKey("JobCategory")]
        //public int JobCategoryID { get; set; }

        //public virtual JobCategory JobCategory { get; set; }
    }
}
