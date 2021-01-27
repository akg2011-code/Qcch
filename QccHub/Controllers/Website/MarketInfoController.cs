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

namespace QccHub.Controllers.Website
{
    public class MarketInfoController : BaseController
    {
        public MarketInfoController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        public async Task<IActionResult> GetAll()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("MarketInfo/GetAll/");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            IEnumerable<MarketInfo> marketInfoList = JsonConvert.DeserializeObject<IEnumerable<MarketInfo>>(body);
            return View(marketInfoList);
        }

        public async Task<IActionResult> GetMarketInfoDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync("MarketInfo/Get/" + id);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            var marketInfoDetails = JsonConvert.DeserializeObject<MarketInfo>(body);
            return View(marketInfoDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MarketInfo model, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("file seems to be empty");
            }

            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("MarketInfo/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            var createdMarketInfo = JsonConvert.DeserializeObject<MarketInfo>(result);
            byte[] fileBytes;
            var multipartContent = new MultipartFormDataContent();

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
                var byteArrayContent = new ByteArrayContent(fileBytes);
                multipartContent.Add(byteArrayContent, "file", file.FileName);
            }

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response2 = await httpClient.PostAsync($"MarketInfo/AddPhoto/{createdMarketInfo.ID}", multipartContent);
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
            var response = await httpClient.DeleteAsync($"MarketInfo/delete/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok();
        }

    }
}
