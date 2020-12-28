using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class JobDetailsVM
    {
        public Job Job { get; set; }
        public List<ApplyJobs> JobApplications { get; set; }
    }
}
