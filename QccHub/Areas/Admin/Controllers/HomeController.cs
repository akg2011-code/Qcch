using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepo;

        public HomeController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
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
        public async Task<IActionResult> Projects()
        {
            ViewData["Companies"] = await _userRepo.GetCompanyUsers();
            return View();
        }
        public IActionResult Companies()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult MarketInfo()
        {
            return View();
        }
    }
}
