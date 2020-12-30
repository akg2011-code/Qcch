using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QccHub.Data.Models
{
    public class CompanyInfo : BaseEntity
    {
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string FoundedYear { get; set; }
        public int CompanyId { get; set; }
        public virtual ApplicationUser Company { get; set; }
    }
}
