using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class NewsComments : BaseEntity, ICreationAuditable, ISoftDeletable
    {
        [ForeignKey("News")]
        public int NewsID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public DateTime Time { get; set; }
        public string Comment { get; set; }

        public virtual News News { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
