using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class ApplyJobs : BaseEntity , ICreationAuditable,ISoftDeletable
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Job")]
        public int JobID { get; set; }
        public string CoverLetter { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string CVFilePath { get; set; }
        public bool IsApproved { get; set; }
        public ApplicationUser User { get; set; }
        public Job Job { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get ; set; }

        public void Approve() => IsApproved = true;
    }
}
