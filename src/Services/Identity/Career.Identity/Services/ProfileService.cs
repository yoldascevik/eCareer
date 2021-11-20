using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Career.Identity.Data;
using Career.Identity.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Career.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CareerIdentityDbContext _dbContext;

        public ProfileService(UserManager<ApplicationUser> userManager, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            CareerIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _dbContext = dbContext;
        }
        
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
                return;
            
            var principal = await _claimsFactory.CreateAsync(user);
 
            List<Claim> claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
            
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            
            context.IsActive = user != null;
        }
    }
}