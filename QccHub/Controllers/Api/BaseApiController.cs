using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QccHub.Data.Extensions;
using QccHub.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QccHub.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public readonly CurrentSession _currentSession;
        public BaseApiController(CurrentSession currentSession, IHttpContextAccessor contextAccessor)
        {
            _currentSession = currentSession;

            var userName = contextAccessor.HttpContext.User.GetUserName();
            var userId = contextAccessor.HttpContext.User.GetUserId();

            _currentSession.UserID = userId != null? int.Parse(userId) : 0;
            _currentSession.UserName = userName ?? "Anonymous";
            
        }
    }
}
