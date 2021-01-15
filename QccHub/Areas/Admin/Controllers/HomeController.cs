using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }
        public IActionResult News() 
        {
            return View();
        }
        public async Task<IActionResult> Jobs()
        {
            ViewData["Companies"] = await _userRepo.GetCompanyUsers();
            return View();
        }
    }
}
