using AppExpedienteDHR.Core.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IRoleService
    {

        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task<RoleViewModel> GetRoleAsync(string id);
        Task<bool> HasRoleAsync(string name);
        Task CreateRoleViewModelAsync(RoleViewModel roleViewModel);
        Task CreateRoleAsync(string roleName);



    }
}
