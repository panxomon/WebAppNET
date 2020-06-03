using System.ComponentModel.DataAnnotations;

namespace Web.App.Areas.Identity.Models
{
    public class RoleViewModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}