using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class Roles : IdentityRole<long, UsuarioRole>
    {
        public Roles() { }
        public Roles(string name)
        {
            Name = name;
        }

        public ICollection<RoleClaim> RoleClaims { get; set; }
    }
}