using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.Helpers;
using QccHub.Hubs;
using QccHub.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Controllers.Api
{
    public class MarketInfoController : BaseApiController
    {
        private readonly IMarketInfoRepository _marketInfoRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public MarketInfoController(IMarketInfoRepository marketRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IWebHostEnvironment hostEnv,
            IHttpContextAccessor contextAccessor,
            IHubContext<NotificationsHub> hubContext) : base(currentSession, contextAccessor)
        {
            _marketInfoRepo = marketRepository;
            _unitOfWork = unitOfWork;
            _hostEnv = hostEnv;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(MarketInfo marketInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _marketInfoRepo.Add(marketInfo);
            await _unitOfWork.SaveChangesAsync();
            await _hubContext.Clients.All.SendCoreAsync("Notify",
                new Object[]
                { new {
                    text = $"New Market Info Added. Click to see details",
                    link = $"/MarketInfo/GetMarketInfoDetails?id={marketInfo.ID}"
                }
            });
            return Created("marketInfo added", marketInfo);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPhoto([FromRoute] int id,[FromForm] IFormFile file)
        {
            var marketInfo = await _marketInfoRepo.GetByIdAsync(id);
            if (marketInfo == null)
            {
                return BadRequest("Not found");
            }

            var validExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!validExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                return BadRequest("File extension is not supported");
            }

            var directoryPath = Path.Combine(_hostEnv.WebRootPath, "MarketInfoImages");
            var result = await FileUploader.Upload(directoryPath, file);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Uploading failed");
            }

            marketInfo.Image = result;
            if (!(await _unitOfWork.SaveChangesAsync() > 0))
            {
                return BadRequest("Uploading failed");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var marketInfo = await _marketInfoRepo.GetAllAsync();
            return Ok(marketInfo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var marketInfo = await _marketInfoRepo.GetByIdAsync(id);
            if (marketInfo == null || marketInfo.IsDeleted == true)
            {
                return NotFound("market info not found");
            }
            return Ok(marketInfo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marketInfo = await _marketInfoRepo.GetByIdAsync(id);
            if (marketInfo == null)
            {
                return NotFound("market info not found");
            }
            _marketInfoRepo.Delete(marketInfo);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
