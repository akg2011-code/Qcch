using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Interfaces
{
    public interface IJobPositionRepository : IGenericRepository<JobPosition>
    {
        Task<JobPosition> GetJobPositionByName(string name);
    }
}
