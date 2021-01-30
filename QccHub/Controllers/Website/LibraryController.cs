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
    public class LibraryController : BaseController
    {
        public LibraryController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        public async Task<IActionResult> GetAll()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("Library/GetAll/");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            IEnumerable<LibraryItem> library = JsonConvert.DeserializeObject<IEnumerable<LibraryItem>>(body);
            return View(library);
        }

        public async Task<IActionResult> GetLibraryItemDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync("Library/Get/" + id);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            var libraryItem = JsonConvert.DeserializeObject<LibraryItem>(body);
            return View(libraryItem);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LibraryItemCreationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data error");
            }

            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Library/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            var createdLibraryItem = JsonConvert.DeserializeObject<LibraryItem>(result);
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
            var response2 = await httpClient.PostAsync($"Library/AddPhoto/{createdLibraryItem.ID}", multipartContent);
            if (!response2.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            using (var ms = new MemoryStream())
            { 
                await model.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
                var byteArrayContent = new ByteArrayContent(fileBytes);
                multipartContent = new MultipartFormDataContent();
                multipartContent.Add(byteArrayContent, "File", model.File.FileName);
            }
            
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response3 = await httpClient.PostAsync($"Library/AddFile/{createdLibraryItem.ID}", multipartContent);
            if (!response3.IsSuccessStatusCode)
            {
                return BadRequest();
            }


            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"Library/delete/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
