using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QccHub.Data;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Hubs;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class JobsController : BaseApiController
    {
        private readonly IJobRepository _jobRepo;
        private readonly IJobApplicationRepository _jobAppRepo;
        private readonly IUserRepository _userRepo;
        private readonly IHubContext<NotificationsHub> _jobsHubContext;
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IJobRepository jobRepo,
                                IJobApplicationRepository jobAppRepo,
                                IUserRepository userRepo,
                                CurrentSession currentSession,
                                IHttpContextAccessor httpContextAccessor,
                                IHubContext<NotificationsHub> jobsHubContext,
                                IUnitOfWork unitOfWork) : base(currentSession,httpContextAccessor)
        {
            _jobRepo = jobRepo;
            _jobAppRepo = jobAppRepo;
            _userRepo = userRepo;
            _jobsHubContext = jobsHubContext;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,company")]
        public async Task<IActionResult> Add(Job job)    
        {
            if (!ModelState.IsValid)                        
            {
                return BadRequest("can't add , some info is wrong");
            }

            _jobRepo.Add(job);               
            await _unitOfWork.SaveChangesAsync();
            return Created("job added", job);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Job job = await _jobRepo.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound("job was not found");
            }

            _jobRepo.Delete(job);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{companyID}")]
        public async Task<IActionResult> GetAllCompanyJobs(int companyID)
        {
            var company = await _userRepo.GetUserByIdAsync(companyID);
            if (company == null)
                return NotFound("No company for this ID");
            
            var companyJobs = await _jobRepo.GetJobsByCompany(companyID);
            return Ok(companyJobs);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var jobDetails = new JobDetailsVM();
            var job = await _jobRepo.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound("No Job for this ID");
            }
            jobDetails.Job = job;

            var jobApplications = await _jobAppRepo.GetJobApplicationsByJob(id);
            if (jobApplications == null)
            {
                jobDetails.JobApplications = new List<ApplyJobs>();
            }
            else
            {
                jobDetails.JobApplications = jobApplications;
            }
            return Ok(jobDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var result = await _jobRepo.GetAllAsync();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllB2bJobs()
        {
            var result = await _jobRepo.GetAllB2bJobs();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllNonB2bJobs()
        {
            var result = await _jobRepo.GetAllNonB2bJobs();
            return Ok(result);
        }


        [HttpGet("{JobName}")]
        public async Task<IActionResult> SearchJobs(string JobName)
        {
            var jobs = await _jobRepo.SearchJobs(JobName);
            return Ok(jobs);
        }
        
        [HttpPost]
        public async Task<IActionResult> ApplyToB2bJob(JobApplication model)
        {
            var newJobApp = model.ToModel();
            _jobAppRepo.Add(newJobApp);
            await _unitOfWork.SaveChangesAsync();
            return Ok(newJobApp.ID);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyToNonB2bJob(JobApplication model) 
        {
            var newJobApp = model.ToModel();
            _jobAppRepo.Add(newJobApp);
            await _unitOfWork.SaveChangesAsync();
            await _jobsHubContext.Clients.User(model.UserID.ToString()).SendAsync("NotifyNewJob", $"/jobs/jobdetails/{model.JobID}");
            return Ok(newJobApp.ID);
        }

        [HttpGet("{jobID}")]
        public async Task<IActionResult> GetAllJobApplications(int jobID)
        {
            var jobApplications = await _jobAppRepo.GetJobApplicationsByJob(jobID);
            return Ok(jobApplications);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJobApplication(JobApprove jobApprove)
        {
            var jobApplication = await _jobAppRepo.GetJobApplicationsByUserAndJob(jobApprove.UserID, jobApprove.JobID);
            var applicant = await _userRepo.GetUserByIdAsync(jobApprove.UserID);

            if (jobApplication == null || applicant == null)
            {
                return NotFound();
            }

            jobApplication.Approve();
            await _unitOfWork.SaveChangesAsync();
            return Ok($"Applicant {applicant.UserName} is approved for job {jobApprove.JobID}");
        }
    }
}