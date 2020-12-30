using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Education : BaseEntity
    {
        public string Institute { get; set; }
        public string Major { get; set; }
        public DateTime Year { get; set; }
        public int EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
