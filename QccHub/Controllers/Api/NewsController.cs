using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Hubs;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class NewsController : BaseApiController
    {
        private readonly INewsRepository _newsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public NewsController(INewsRepository newsRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IHttpContextAccessor contextAccessor,
            IHubContext<NotificationsHub> hubContext) : base(currentSession, contextAccessor)
        {
            _newsRepository = newsRepository;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _newsRepository.Add(news);
            await _unitOfWork.SaveChangesAsync();

            await _hubContext.Clients.All.SendCoreAsync("Notify",
                new Object[]
                { new {
                    text = $"News added. Click to see details",
                    link = $"/News/GetNewsDetails?id={news.ID}"
                }
            });
            return Created("news added", news);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsRepository.GetAllAsync();
            return Ok(news);
        }

        [HttpGet("{newsID}")]
        public async Task<IActionResult> GetNewsByID(int newsID)
        {
            var news =await _newsRepository.GetByIdAsync(newsID);
            if (news == null || news.IsDeleted == true)
            {
                return NotFound("no news for this ID");
            }
            return Ok(news);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound("New not found");
            }
            _newsRepository.Delete(news);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}