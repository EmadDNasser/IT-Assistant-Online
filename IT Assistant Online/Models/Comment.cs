using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Assistant_Online.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [Required]
        public string Contenet { get; set; }

        public DateTime Date { get; set; }

        public int PostID { get; set; }

        public virtual Post Post { get; set; }

        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}