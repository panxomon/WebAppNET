using Microsoft.AspNet.Identity;

namespace Web.Base.Identity.Core
{
    public class UserValidator : UserValidator<Usuario, long>
    {
        public UserValidator(UserManager<Usuario, long> manager) : base(manager)
        {
        }
    }
}
