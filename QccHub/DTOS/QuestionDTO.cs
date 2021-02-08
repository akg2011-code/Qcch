using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class QuestionDTO
    {
        [Required]
        public string Title { get; set; }
        public int UserID { get; set; }
    }
}
