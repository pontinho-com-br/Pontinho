using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace Pontinho.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<IdentityResult> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            var userIdentity = await manager.CreateAsync(this);
            // Add custom user claims here
            return userIdentity;
        }
    }
}