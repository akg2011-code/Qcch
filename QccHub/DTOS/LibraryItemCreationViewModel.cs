
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QccHub.DTOS
{
    public class LibraryItemCreationViewModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Details { get; set; }
        
        [Required]
        [JsonIgnore]
        public IFormFile Image { get; set; }
        
        [Required]
        [JsonIgnore]
        public IFormFile File { get; set; }
    }
}
