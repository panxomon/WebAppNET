using System.Collections.Generic;
//using System.Web.Mvc;

namespace Web.App.Areas.Identity.Models
{
    public class UsuarioRolesViewModel
    {
        public UsuarioRolesViewModel()
        {
            UserRoles = new List<SelectListItem>();
            SelectedRoles = new List<string>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public List<SelectListItem> UserRoles { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}