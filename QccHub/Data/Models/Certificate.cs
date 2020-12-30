using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.Data.Models
{
    public class Certificate : BaseEntity
    {
        public string CourseName { get; set; }
        public string Institute { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CertificateImagePath { get; set; }
        public int EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
