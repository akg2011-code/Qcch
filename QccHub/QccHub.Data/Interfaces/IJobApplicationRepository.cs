using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using QccHub.Data.Models;

namespace QccHub.Data.Interfaces
{
    public interface IJobApplicationRepository : IGenericRepository<ApplyJobs>
    {
        Task<List<ApplyJobs>> GetJobApplicationsByJob(int jobID);
        Task<ApplyJobs> GetJobApplicationsByUserAndJob(int userId, int jobId);
    }
}
