using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace QccHub.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCertificate(Certificate certificate)
        {
            _context.Add(certificate);
        }

        public void AddEducation(Education education)
        {
            _context.Add(education);
        }

        public void AddNewCompany(ApplicationUser company)
        {
            _context.ApplicationUser.Add(company);
        }

        public void Delete(ApplicationUser user)
        {
            _context.ApplicationUser.Remove(user);
        }

        public Task<List<ApplicationUser>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }

        public Task<ApplicationUser> GetCompanyByName(string name)
        {
            return _context.Users
                            .Include(u => u.CompanyInfo)
                            .Include(u => u.Country)
                            .IncludeFilter(u => u.UserRoles.Where(ur => ur.RoleId == (int)RolesEnum.Company)).FirstOrDefaultAsync(u => u.CompanyName == name);
        }

        public Task<List<ApplicationUser>> GetCompanyUsers()
        {
            return _context.Users
                            .Include(u => u.CompanyInfo)
                            .Include(u => u.Country)
                            .IncludeFilter(u => u.UserRoles.Where(ur => ur.RoleId == (int)RolesEnum.Company)).ToListAsync();
        }

        public Task<UserJobPosition> GetCurrentJobPosition(int userId)
        {
            return _context.UserJobPositions
                            .Include(ujp => ujp.JobPosition)
                            .IncludeFilter(ujp => ujp.Employee.UserRoles.FirstOrDefault(r => r.RoleId == (int)RolesEnum.User))
                            .FirstOrDefaultAsync(ujp => ujp.IsCurrentPosition && ujp.EmployeeId == userId);
        }

        public Task<List<ApplicationUser>> GetEmployeeUsers()
        {
            return _context.Users
                            .Include(u => u.Country)
                            .IncludeFilter(u => u.UserRoles.Where(ur => ur.RoleId == (int)RolesEnum.User)).ToListAsync();
        }

        public Task<ApplicationUser> GetUserByIdAsync(int userId)
        {
            return _context.Users.Include(u => u.UserRoles)
                                    .Include(u => u.Country)
                                    .Include(u => u.JobOffers)
                                    .Include(u => u.Education)
                                    .Include(u => u.Projects)
                                    .Include(u => u.Certificates)
                                    //.ThenInclude(jo => jo.JobCategory)
                                    .Include(u => u.CompanyInfo)
                                    .Include(u => u.EmployeeJobs)
                                    .Include("EmployeeJobs.Company")
                                    .Include("EmployeeJobs.JobPosition")
                                    .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Email == userName);
        }

        public async Task<int> GetUserRole(int userId)
        {
            return (await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId)).RoleId;
        }
    }
}
