using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class File
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Extension { get; set; }
        public long Size { get; set; }

        public Folder Folder { get; set; }
        public Guid FolderId { get; set; }
    }
}
