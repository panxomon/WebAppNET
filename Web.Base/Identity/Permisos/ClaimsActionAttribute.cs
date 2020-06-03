using System;

namespace Web.Base.Identity.Permisos
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ClaimsActionAttribute : Attribute // TODO: Esta clase debe ser virtual o estar definida con una interface!!!! FM
    {
        public ClaimsActions Claim { get; private set; }

        public ClaimsActionAttribute(ClaimsActions claim)
        {
            Claim = claim;
        }
    }
}
