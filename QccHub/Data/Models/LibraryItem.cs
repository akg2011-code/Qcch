using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class LibraryItem : BaseEntity , ICreationAuditable, ISoftDeletable
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public string File { get; set; }
        public int CreatedBy { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
