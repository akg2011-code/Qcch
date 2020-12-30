using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class JobPositionDTO
    {
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCurrent  { get; set; }
    }
}
