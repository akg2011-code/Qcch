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
    public class JobPositionRepository : GenericRepository<JobPosition>, IJobPositionRepository
    {
        private readonly ApplicationDbContext _context;

        public JobPositionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<JobPosition> GetJobPositionByName(string name)
        {
            return _context.JobPositions.FirstOrDefaultAsync(jp => jp.Name == name);
        }
    }
}
