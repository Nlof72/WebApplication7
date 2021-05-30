using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication7.Models
{
    public class FileView
    {
        public string Name { get; set; }
        public IFormFile FilePath { get; set; }

    }
}
