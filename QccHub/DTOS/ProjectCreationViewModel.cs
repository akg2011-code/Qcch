using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class ProjectCreationViewModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Details { get; set; }

        [JsonIgnore]
        public IFormFile Image { get; set; }
    }
}
