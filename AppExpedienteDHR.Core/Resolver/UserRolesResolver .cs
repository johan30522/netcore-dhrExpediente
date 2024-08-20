using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.ViewModels.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace AppExpedienteDHR.Core.Resolver
{
    public class UserRolesResolver : IValueResolver<ApplicationUser, UserViewModel, IEnumerable<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesResolver(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<string> Resolve(ApplicationUser source, UserViewModel destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            var roles = _userManager.GetRolesAsync(source).Result;
            return roles;
        }
    }
}
