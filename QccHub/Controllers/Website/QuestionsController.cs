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
    public class QuestionsController : BaseController
    {
        public QuestionsController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync("Questions/GetAllQuestions");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }

            var questions = JsonConvert.DeserializeObject<List<Question>>(body);
            return View(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> QuestionDetails(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.GetAsync($"Questions/GetQuestion/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "home");
            }
            var question = JsonConvert.DeserializeObject<Question>(body);
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Add(QuestionDTO model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Questions/add", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.DeleteAsync($"Questions/Delete/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }
            return Ok();
        }

        [HttpPost("{questionId}")]
        public async Task<IActionResult> AddAnswer(int questionId, AnswerDTO model)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"Questions/addanswer/{questionId}", jsonContent);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(result);
            }

            var createdAnswer = JsonConvert.DeserializeObject<Answers>(result);
            return Ok(createdAnswer);
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                     