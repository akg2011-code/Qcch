using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Notification>> GetNotificationsByUserId(int userId)
        {
            return context.Notifications.Where(u => u.UserId == userId || u.UserId == 0)
                                        .Take(10)
                                        .OrderBy(n => n.IsSeen)
                                        .OrderByDescending(n => n.CreatedDate)
                                        .ToListAsync();
        }
    }
}
