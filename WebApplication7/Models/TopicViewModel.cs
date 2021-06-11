using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApplication7.Models
{
    public class TopicViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Text { get; set; }

        public IFormFile Image1 { get; set; }

        public IFormFile Image2 { get; set; }

        public IFormFile Image3 { get; set; }
    }
}
