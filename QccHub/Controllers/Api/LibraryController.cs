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
    public class LibraryController : BaseApiController
    {
        private readonly ILibraryRepository _libraryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public LibraryController(ILibraryRepository libraryRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IWebHostEnvironment hostEnv,
            IHttpContextAccessor contextAccessor,
            IHubContext<NotificationsHub> hubContext) : base(currentSession, contextAccessor)
        {
            _libraryRepo = libraryRepository;
            _unitOfWork = unitOfWork;
            _hostEnv = hostEnv;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add(LibraryItem libraryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _libraryRepo.Add(libraryItem);
            await _unitOfWork.SaveChangesAsync();
            await _hubContext.Clients.All.SendCoreAsync("Notify",
                new Object[]
                { new {
                    text = $"New item was added to the library. Click to see details",
                    link = $"/Library/GetLibraryItemDetails?id={libraryItem.ID}"
                }
            });
            return Created("item added", libraryItem);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPhoto([FromRoute] int id, [FromForm] IFormFile image)
        {
            var libraryItem = await _libraryRepo.GetByIdAsync(id);
            if (libraryItem == null)
            {
                return BadRequest("Not found");
            }

            var validExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!validExtensions.Contains(Path.GetExtension(image.FileName)))
            {
                return BadRequest("File extension is not supported");
            }

            var directoryPath = Path.Combine(_hostEnv.WebRootPath, "LibraryImages");
            var result = await FileUploader.Upload(directoryPath, image);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Uploading failed");
            }

            libraryItem.Image = result;
            if (!(await _unitOfWork.SaveChangesAsync() > 0))
            {
                return BadRequest("Uploading failed");
            }

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddFile([FromRoute] int id, [FromForm] IFormFile file)
        {
            var libraryItem = await _libraryRepo.GetByIdAsync(id);
            if (libraryItem == null)
            {
                return BadRequest("Not found");
            }

            var validExtensions = new string[] { ".pdf", ".xlsx", ".xls", ".docx", ".doc", ".txt" };
            if (!validExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                return BadRequest("File extension is not supported");
            }

            var directoryPath = Path.Combine(_hostEnv.WebRootPath, "LibraryFiles");
            var result = await FileUploader.Upload(directoryPath, file);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Uploading failed");
            }

            libraryItem.File = result;
            if (!(await _unitOfWork.SaveChangesAsync() > 0))
            {
                return BadRequest("Uploading failed");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraryItem = await _libraryRepo.GetAllAsync();
            return Ok(libraryItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var libraryItem = await _libraryRepo.GetByIdAsync(id);
            if (libraryItem == null || libraryItem.IsDeleted == true)
            {
                return NotFound("item not found");
            }
            return Ok(libraryItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var libraryItem = await _libraryRepo.GetByIdAsync(id);
            if (libraryItem == null)
            {
                return NotFound("item not found");
            }
            _libraryRepo.Delete(libraryItem);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
