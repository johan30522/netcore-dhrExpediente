using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Role;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace AppExpedienteDHR.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger logger)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }



        public async Task CreateRoleAsync(string roleName)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating role");
                throw;
            }
        }

        public async Task CreateRoleViewModelAsync(RoleViewModel roleViewModel)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating role");
                throw;
            }
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        {
            try
            {
                var roles = _roleManager.Roles;
                return _mapper.Map<IEnumerable<RoleViewModel>>(roles);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all roles");
                throw;
            }


        }

        public async Task<RoleViewModel> GetRoleAsync(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                return _mapper.Map<RoleViewModel>(role);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting role");
                throw;
            }
        }



        public async Task<bool> HasRoleAsync(string name)
        {
            try
            {
                return await _roleManager.RoleExistsAsync(name);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error checking role");
                throw;
            }
        }
    }
}
