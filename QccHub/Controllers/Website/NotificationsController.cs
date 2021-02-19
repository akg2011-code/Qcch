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
    public class NotificationsController : BaseController
    {
        public NotificationsController(IConfiguration iConfig, IHttpClientFactory clientFactory) : base(iConfig, clientFactory)
        {
        }

        public async Task<IActionResult> Get(int userId)
        {
            var httpClient = _clientFactory.CreateClient("API");
            var response = await httpClient.GetAsync("Notifications/Get/" + userId);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return PartialView("_NotificationPartial", new List<Notification>());
            }

            var notifications = JsonConvert.DeserializeObject<List<Notification>>(body);
            return PartialView("_NotificationPartial", notifications);
        }

    }
}
