using System.ComponentModel.DataAnnotations;

namespace Web.App.Areas.Identity.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Rut")]
        public string Rut { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}