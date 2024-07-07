using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IT_Assistant_Online.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "حقل العنوان مطلوب")]
        [Display(Name = "العنوان")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "المحتوى مطلوب")]
        [Display(Name = "المحتوى")]
        public string Contenet { get; set; }
        [Display(Name = "صورة")]
        public string Image { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime Date { get; set; }

        public bool IsSolved { get; set; }
        public string UserID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        public virtual ICollection<Category> Categories { get; set; }

        public int SectionId { get; set; }

    }
}