using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_Assistant_Online.Models
{
    public class Articl
    {
        public int id { get; set; }

        [Required(ErrorMessage = "عنوان المقالة مطلوب")]
        [Display(Name = "عنوان المقالة")]
        public string Title { get; set; }


        [AllowHtml]
        [Required(ErrorMessage ="محتوى المقالة مطلوب")]
        [Display(Name = "محتوى المقالة")]
        public string Body { get; set; }

        [Required(ErrorMessage = "حقل الصورة مطلوب")]
        [Display(Name ="صورة")]
        public string ArticleImage { get; set; }

        [Required]
        [Display(Name ="تاريخ المقالة")]
        public DateTime Date { get; set; }
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}