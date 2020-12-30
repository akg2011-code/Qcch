using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Course : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public string Name { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public string Inistitute { get; set; }
        public string CertifiedFilePath { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
