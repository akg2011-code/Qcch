using Microsoft.AspNetCore.Http;
using QccHub.Data;
using QccHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class JobApplication
    {
        public int UserID { get; set; }
        public int JobID { get; set; }
        public string CoverLetter { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal ExpectedSalary { get; set; }
        public ApplyJobs ToModel()
        {
            return new ApplyJobs()
            {
                ExpectedSalary = this.ExpectedSalary,
                CurrentSalary = this.CurrentSalary,
                UserID = this.UserID,
                JobID = this.JobID,
                CoverLetter = this.CoverLetter
            };
        }
    }
}