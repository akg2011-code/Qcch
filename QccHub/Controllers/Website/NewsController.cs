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

namespace QccHub.Controllers.Website
{
    public class NewsController : BaseController
    {
        public NewsController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        public async Task<IActionResult> GetAllNews()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("News/GetAllNews/");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index","home");
            }

            IEnumerable<News> news = JsonConvert.DeserializeObject<IEnumerable<News>>(body);
            return View(news);
        }

        public async Task<IActionResult> GetNewsDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync("News/GetNewsByID/" + id);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            News news = JsonConvert.DeserializeObject<News>(body);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Add(News model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("News/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}
