using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class UpdateInfoVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? GenderID { get; set; }
        public int NationalityID { get; set; }
        public string Bio  { get; set; }
        public string Position  { get; set; }
        public string ProfileImagePath { get; set; }
        public int RoleId { get; set; }

        public string Mission { get; set; }
        public string Vision { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string FoundedYear { get; set; }

    }
}
