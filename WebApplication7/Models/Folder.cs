using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class Folder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public Guid? FolderId { get; set; }
        public ICollection<Folder> Folders { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
