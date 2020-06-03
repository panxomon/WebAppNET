using System.ComponentModel.DataAnnotations;

namespace Web.App.Areas.Identity.Permisos
{
    public enum Recursos
    {
        [Display(Name = "Usuario#Permisos")]
        Usuario = 1,

        [Display(Name = "Roles#Permisos")]
        Roles = 2,

        [Display(Name = "Reclamaciones#Permisos")]
        Claims = 3,

        [Display(Name = "Cuenta#Permisos")]
        Account = 4,

        [Display(Name = "Usuario#login")]
        Login = 5
    }
}