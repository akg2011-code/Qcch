using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Qualification : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public string Title { get; set; }
        public string Details { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
