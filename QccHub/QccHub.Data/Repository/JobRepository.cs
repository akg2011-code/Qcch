using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        private ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Job>> SearchJobs(string jobName)
        {
            return _context.Job
                            .Where(j => j.Title.Contains(jobName))
                            //.Include(j=>j.JobCategory)
                            .Include(j=>j.Company)
                            .ToListAsync();
        }

        public Task<List<Job>> GetJobsByCompany(int companyId)
        {
            return _context.Job
                            .Where(j => j.CompanyID == companyId)
                            //.Include(j => j.JobCategory)
                            .Include(j => j.Company)
                            .ToListAsync();
        }

        public override Task<List<Job>> GetAllAsync()
        {
            return _context.Job
                            .Include(j => j.Company)
                            //.Include(j => j.JobCategory)
                            .ToListAsync();
        }

        public override Task<Job> GetByIdAsync(int id)
        {
            return _context.Job
                            .Include(j => j.Company)
                            //.Include(j => j.JobCategory)
                            .FirstOrDefaultAsync(j => j.ID == id);
        }
        public Task<List<Job>> GetAllB2bJobs()
        {
            return _context.Job
                            .Where(j => j.IsB2B)
                            .Include(j => j.Company)
                            //.Include(j => j.JobCategory)
                            .ToListAsync();
        }

        public Task<List<Job>> GetAllNonB2bJobs()
        {
            return _context.Job.Where(j => !j.IsB2B)
                            .Include(j => j.Company)
                            //.Include(j => j.JobCategory)
                            .ToListAsync();
        }
    }
}
