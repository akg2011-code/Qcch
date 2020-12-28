using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByIdAsync(int userId);
        Task<List<ApplicationUser>> GetAllUsers();
        Task<List<ApplicationUser>> GetCompanyUsers();
        Task<List<ApplicationUser>> GetEmployeeUsers();
        Task<int> GetUserRole(int userId);
        UserJobPosition GetCurrentJobPosition(int userId);
        Task<ApplicationUser> GetCompanyByName(string name);
        void AddNewCompany(ApplicationUser company);
    }
}
