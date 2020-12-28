    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QccHub.Data.Extensions;
using QccHub.Data.Models;
using QccHub.Logic.Helpers;

namespace QccHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
                                                            ApplicationRole,
                                                            int,
                                                            ApplicationUserClaim,
                                                            ApplicationUserRole,
                                                            ApplicationUserLogin,
                                                            ApplicationRoleClaim,
                                                            ApplicationUserToken>
    {
        private readonly CurrentSession currentSession;

        private ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, CurrentSession currentSession) : this(options)
        {
            this.currentSession = currentSession;
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> AppRole { get; set; }
        public DbSet<ApplicationUserClaim> AppUserClaim { get; set; }
        public DbSet<ApplicationUserRole> AppUserRole { get; set; }
        public DbSet<ApplicationUserLogin> AppUserLogin { get; set; }
        public DbSet<ApplicationRoleClaim> AppRoleClaims { get; set; }
        public DbSet<ApplicationUserToken> AppUserTokens { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<ApplyJobs> ApplyJobs { get; set; }
        public DbSet<ItemsCategory> ItemsCategory { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobCategory> JobCategory { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsComments> NewsComments { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<UserJobPosition> UserJobPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region ApplicationUserConfiguration

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.UserRoles)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.CompanyInfo)
                .WithOne(b => b.Company)
                .HasForeignKey<CompanyInfo>(b => b.CompanyId).OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ApplicationUserRoleConfiguration

            builder.Entity<ApplicationUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<ApplicationUserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUserRole>().HasOne(x => x.ApplicationUser).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            #endregion

            builder.Entity<ApplyJobs>().HasOne(x => x.User).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserJobPosition>().HasKey(x => new { x.CompanyId, x.EmployeeId, x.JobPositionId , x.FromDate , x.ToDate });
            builder.Entity<UserJobPosition>().HasOne(x => x.Employee).WithMany(x => x.EmployeeJobs).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserJobPosition>().HasOne(x => x.Company).WithMany(x => x.CompanyJobs).HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserJobPosition>().HasOne(x => x.JobPosition).WithMany(x => x.EmployeeJobs).HasForeignKey(x => x.JobPositionId).OnDelete(DeleteBehavior.NoAction);

            builder.ConfigureShadowProperties();
            builder.SetGlobalQueryFilters();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.SetShadowProperties(currentSession);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
