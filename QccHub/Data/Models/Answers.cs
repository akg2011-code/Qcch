using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using QccHub.Data.Interfaces;

namespace QccHub.Data.Models
{
    public class Answers : BaseEntity , ICreationAuditable, ISoftDeletable
    {
        public string Text { get; set; }
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual Question Question { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get ; set; }
        public bool IsDeleted { get; set; }
    }
}
