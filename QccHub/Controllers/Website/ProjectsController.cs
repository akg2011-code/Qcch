using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QccHub.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using QccHub.DTOS;

namespace QccHub.Controllers.Website
{
    public class ProjectsController : BaseController
    {
        public ProjectsController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        public async Task<IActionResult> GetAll()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("Projects/GetAll/");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            IEnumerable<Project> projects = JsonConvert.DeserializeObject<IEnumerable<Project>>(body);
            return View(projects);
        }

        public async Task<IActionResult> GetProjectDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync("Projects/Get/" + id);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            var project = JsonConvert.DeserializeObject<Project>(body);
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectCreationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data error");
            }

            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Projects/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            var createdProject = JsonConvert.DeserializeObject<Project>(result);
            byte[] fileBytes;
            var multipartContent = new MultipartFormDataContent();

            using (var ms = new MemoryStream())
            {
                await model.Image.CopyToAsync(ms);
                fileBytes = ms.ToArray();
                var byteArrayContent = new ByteArrayContent(fileBytes);
                multipartContent.Add(byteArrayContent, "Image", model.Image.FileName);
            }

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response2 = await httpClient.PostAsync($"Projects/AddPhoto/{createdProject.ID}", multipartContent);
            if (!response2.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"Projects/delete/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
