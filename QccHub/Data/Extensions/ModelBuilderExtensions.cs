using Microsoft.EntityFrameworkCore;
using QccHub.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QccHub.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static readonly MethodInfo SetIsDeletedShadowPropertyMethodInfo = typeof(ModelBuilderExtensions).GetMethod(nameof(SetIsDeletedShadowProperty));

        private static readonly MethodInfo SetCreatedAuditingShadowPropertiesMethodInfo = typeof(ModelBuilderExtensions).GetMethod(nameof(SetCreatedAuditingShadowProperties));
        
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(ModelBuilderExtensions).GetMethod(nameof(SetGlobalQueryForSoftDelete));

        public static void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDeletable
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }

        public static void ConfigureShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                // set auditing properties
                if (typeof(ICreationAuditable).IsAssignableFrom(t))
                {
                    var method = SetCreatedAuditingShadowPropertiesMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }

                if (typeof(ISoftDeletable).IsAssignableFrom(t))
                {
                    var method = SetIsDeletedShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }
        public static void SetIsDeletedShadowProperty<T>(ModelBuilder builder) where T : class, ISoftDeletable
        {
            // define shadow property
            builder.Entity<T>().Property(x => x.IsDeleted);
        }

        public static void SetCreatedAuditingShadowProperties<T>(ModelBuilder builder) where T : class, ICreationAuditable
        {
            // define shadow properties
            builder.Entity<T>().Property("CreatedDate").HasDefaultValueSql("GetUtcDate()");
            builder.Entity<T>().Property("CreatedBy").HasMaxLength(256).HasDefaultValueSql(null);
        }

        public static void SetGlobalQueryFilters(this ModelBuilder builder) 
        {
            foreach (var tp in builder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                if (typeof(ISoftDeletable).IsAssignableFrom(t))
                {
                    var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                    method.Invoke(builder, new object[] { builder });
                }
            }
        }
    }
}
