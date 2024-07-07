using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Assistant_Online.Models
{
    public class Section
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="اسم القسم")]
        public string Name { get; set; }
    }
}