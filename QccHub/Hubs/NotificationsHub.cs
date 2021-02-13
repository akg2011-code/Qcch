using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task NotifyNewJob(string userId, string link) 
        {
            await Clients.User(userId).SendAsync("NotifyNewJob",userId,link);
        }
    }
}
