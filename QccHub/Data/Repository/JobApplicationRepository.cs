using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class JobApplicationRepository : GenericRepository<ApplyJobs> , IJobApplicationRepository
    {
        private ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<ApplyJobs>> GetJobApplicationsByJob(int jobID)
        {
            return _context.ApplyJobs.Where(j => j.JobID == jobID).Include(j => j.User).Include(j => j.Job).ToListAsync();
        }

        public Task<ApplyJobs> GetJobApplicationsByUserAndJob(int userId, int jobId)
        {
            return _context.ApplyJobs.FirstOrDefaultAsync(j => j.JobID == jobId && j.UserID == userId);
        }

    }
}
