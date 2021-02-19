using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using QccHub.DTOS;
using QccHub.Logic.Helpers;

namespace QccHub.Controllers.Api
{
    public class NotificationsController : BaseApiController
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(
                                CurrentSession currentSession,
                                IHttpContextAccessor httpContextAccessor,
                                INotificationRepository notificationRepository,
                                IUnitOfWork unitOfWork) : base(currentSession, httpContextAccessor)
        {
            _notificationRepo = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var notifications = await _notificationRepo.GetNotificationsByUserId(userId);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MarkAsSeen(int id)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);
            if (notification == null)
            {
                return BadRequest();
            }

            notification.IsSeen = true;
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}
