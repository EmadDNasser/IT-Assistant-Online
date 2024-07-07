using System.ComponentModel.DataAnnotations;

namespace IT_Assistant_Online.Models
{
    public class Location
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public double Long { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public double Lat { get; set; }
        [Required(ErrorMessage = "مطلوب")]
        public string Message { get; set; }
        public string Section { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool IsRead { get; set; }

    }
}