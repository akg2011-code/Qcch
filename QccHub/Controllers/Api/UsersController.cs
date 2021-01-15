using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _UserRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
                                CurrentSession currentSession,
                                IHttpContextAccessor httpContextAccessor,
                                IUserRepository UsersRepository,
                                IUnitOfWork unitOfWork) : base(currentSession, httpContextAccessor)
        {
            _UserRepo = UsersRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _UserRepo.GetCompanyUsers();
            return Ok(companies);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var companies = await _UserRepo.GetEmployeeUsers();
            return Ok(companies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _UserRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("user was not found");
            }
            _UserRepo.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
