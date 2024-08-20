using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Role;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AppExpedienteDHR.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public async Task CreateRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task CreateRoleViewModelAsync(RoleViewModel roleViewModel)
        {
            var role = new IdentityRole
            {
                Name = roleViewModel.Name
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles;
            return _mapper.Map<IEnumerable<RoleViewModel>>(roles);

           
        }

        public async Task<RoleViewModel> GetRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return _mapper.Map<RoleViewModel>(role);
        }



        public async Task<bool> HasRoleAsync(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }
    }
}
