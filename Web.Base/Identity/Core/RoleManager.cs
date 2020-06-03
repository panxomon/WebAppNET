using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Base.Identity.Core
{
    public class RoleManager : RoleManager<Roles, long>
    {
        private readonly ApiUserContext _context;

        public RoleManager(ApiUserContext context) : base(new RoleStore<Roles, long, UsuarioRole>(context))  
        {
            _context = context;
        }

        public async Task<List<Claim>> GetClaimsAsync(string roleName)
        {
            var roleClaims = await _context.RoleClaim.Where(rc => rc.Role.Name == roleName).ToListAsync();

            var claims = roleClaims
                .Select(rc => new Claim(rc.ClaimType, rc.ClaimValue))
                .ToList();

            return claims;
        }

        public async Task AddClaimAsync(long roleId, Claim claim)
        {
            var roleClaim = new RoleClaim()
            {
                RoleId = roleId,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };

            _context.RoleClaim.Add(roleClaim);

            await _context.SaveChangesAsync();
        }


        public async Task RemoveClaimAsync(long roleId, Claim claim)
        {
            var claimToRemove = await _context.RoleClaim.FirstOrDefaultAsync(rc => rc.RoleId == roleId && rc.ClaimType == claim.Type && rc.ClaimValue == claim.Value);

            if (claimToRemove != null)
            {
                _context.RoleClaim.Remove(claimToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}