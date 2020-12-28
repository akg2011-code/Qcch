using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QccHub.Data.Interfaces;
using QccHub.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Data.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetShadowProperties(this ChangeTracker changeTracker, CurrentSession userSession)
        {
            changeTracker.DetectChanges();
            foreach (var entry in changeTracker.Entries())
            {
                if (entry.Entity is ICreationAuditable)
                {
                    if (entry.State == EntityState.Added && entry.Property("CreatedBy").CurrentValue.ToString() == "0")
                    {
                        entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                        entry.Property("CreatedBy").CurrentValue = userSession.UserID;
                    }
                }
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeletable)
                {
                    entry.State = EntityState.Modified;
                    entry.Property("IsDeleted").CurrentValue = true;
                }
            }
        }
    }
}
