using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT_Assistant_Online.Models
{

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }


    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "أدخل اسم المستخدم!")]
        [Display(Name = "اسم المستخدم:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "أدخل كلمة المرور!")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور:")]
        public string Password { get; set; }

        [Display(Name = "تذكر كلمة المرور")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "حقل اسم المستخدم مطلوب")]
        [Display(Name = "اسم المستخدم:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "حقل البريد الالكتروني مطلوب")]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني:")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "رقم الهاتف:")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "حقل كلمة المرور مطلوب")]
        [StringLength(100, ErrorMessage = "كلمة المرور غير صالحة", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور:")]
        [Compare("Password", ErrorMessage = "كلمتي المرور غير متطابقتين")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
