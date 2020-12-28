using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class BasicInfoVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NationalityID { get; set; }
        public int GenderID { get; set; }
    }
}
