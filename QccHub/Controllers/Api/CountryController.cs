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
    public class CountryController : BaseApiController
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(
                                CurrentSession currentSession,
                                IHttpContextAccessor httpContextAccessor,
                                ICountryRepository countryRepository) : base(currentSession, httpContextAccessor)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryRepository.GetAllAsync();
            return Ok(countries);
        }
    }
}
