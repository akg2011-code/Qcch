using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Notification : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSeen { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
