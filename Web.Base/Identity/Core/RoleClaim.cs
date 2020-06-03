namespace Web.Base.Identity.Core
{
    public class RoleClaim
    {
        public long  Id { get; set; } 
        public long RoleId { get; set; }
        public Roles Role { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}