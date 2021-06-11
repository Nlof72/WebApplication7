using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class TopicCreatorModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string CreateDate { get; set; }
        public string EditDate { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

    }
}
