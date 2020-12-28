using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QccHub.Data.Models;
using QccHub.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QccHub.Controllers.Website
{
    public class JobsController : BaseController
    {
        public JobsController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNonB2bJobs()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("Jobs/GetAllNonB2bJobs");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            List<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(body);
            return View("GetAllJobs", jobs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllB2bJobs()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("Jobs/GetAllB2bJobs");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            List<Job> jobs = JsonConvert.DeserializeObject<List<Job>>(body);
            return View("GetAllJobs", jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> JobDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync($"Jobs/GetJob/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }
            var jobDetails = JsonConvert.DeserializeObject<JobDetailsVM>(body);
            return View(jobDetails);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Job model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Jobs/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyToNonB2bJob(JobApplication model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Jobs/ApplyToNonB2bJob", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> ApplyToB2bJob(JobApplication model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Jobs/ApplyToB2bJob", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            
            return RedirectToAction("index", "home");
        }

    }
}
