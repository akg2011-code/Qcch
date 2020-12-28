using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class CompanyOverviewInfo
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string FoundedYear { get; set; }
    }
}
