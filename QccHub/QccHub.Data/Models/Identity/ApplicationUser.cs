using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QccHub.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsTrusted { get; set; } // for companies
        public string LigitDocument { get; set; } // for companies
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("Gender")]
        public int? GenderID { get; set; }
        [ForeignKey("Country")]
        public int? NationalityID { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string CVFilePath { get; set; }
        public string ProfileImagePath { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Country Country { get; set; }
        public virtual CompanyInfo CompanyInfo { get; set; }
        public virtual ICollection<UserJobPosition> EmployeeJobs { get; } = new List<UserJobPosition>();
        public virtual ICollection<UserJobPosition> CompanyJobs { get; } = new List<UserJobPosition>();
        public virtual ICollection<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
        public virtual ICollection<Job> JobOffers { get; } = new List<Job>();

        public void ResetJobPositions() 
        {
            foreach (var job in EmployeeJobs)
            {
                job.IsCurrentPosition = false;
            }
        }

        public void SetCommonData(string email, string phoneNumber) 
        {
            UserName = email;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void AddNewJobByName(string name, int companyId)
        {
            var newJobPosition = new UserJobPosition
            {
                CompanyId = companyId,
                EmployeeId = Id,
                FromDate = DateTime.UtcNow,
                IsCurrentPosition = true,
                JobPosition = new JobPosition
                { Name = name }
            };
            EmployeeJobs.Add(newJobPosition);
        }


        public void UpdateBasicCompanyInfo(string mission, string vision) 
        {
            if (CompanyInfo == null)
            {
                CompanyInfo = new CompanyInfo { CompanyId = Id };
            }

            CompanyInfo.Mission = mission;
            CompanyInfo.Vision = vision;
        }

        public void UpdateCompanyOverview(string website, string type, string industry, string size, string foundedYear) 
        {
            if (CompanyInfo == null)
            {
                CompanyInfo = new CompanyInfo { CompanyId = Id} ;
            }

            CompanyInfo.Website = website;
            CompanyInfo.Type = type;
            CompanyInfo.Industry = industry;
            CompanyInfo.Size = size;
            CompanyInfo.FoundedYear = foundedYear;
        }

        public void AddToRole(int roleId) 
        {
            var userRole = new ApplicationUserRole { RoleId = roleId };
            UserRoles.Add(userRole);
        }
    }
}
