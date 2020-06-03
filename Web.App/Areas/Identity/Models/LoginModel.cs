using System.ComponentModel.DataAnnotations;

namespace Web.App.Areas.Identity.Models
{
    public class LoginModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
            
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
    }
}