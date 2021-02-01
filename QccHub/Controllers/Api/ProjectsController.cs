using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.Helpers;
using QccHub.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Controllers.Api
{
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectRepository _projectsRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnv;

        public ProjectsController(IProjectRepository projectRepository,
            IUnitOfWork unitOfWork,
            CurrentSession currentSession,
            IWebHostEnvironment hostEnv,
            IHttpContextAccessor contextAccessor) : base(currentSession, contextAccessor)
        {
            _projectsRepo = projectRepository;
            _unitOfWork = unitOfWork;
            _hostEnv = hostEnv;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _projectsRepo.Add(project);
            await _unitOfWork.SaveChangesAsync();
            return Created("Project added", project);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPhoto([FromRoute] int id, [FromForm] IFormFile image)
        {
            var project = await _projectsRepo.GetByIdAsync(id);
            if (project == null)
            {
                return BadRequest("Not found");
            }

            var validExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!validExtensions.Contains(Path.GetExtension(image.FileName)))
            {
                return BadRequest("File extension is not supported");
            }

            var directoryPath = Path.Combine(_hostEnv.WebRootPath, "ProjectsImages");
            var result = await FileUploader.Upload(directoryPath, image);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Uploading failed");
            }

            project.Image = result;
            if (!(await _unitOfWork.SaveChangesAsync() > 0))
            {
                return BadRequest("Uploading failed");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectsRepo.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectsRepo.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound("Project not found");
            }
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectsRepo.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound("Project not found");
            }
            _projectsRepo.Delete(project);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
