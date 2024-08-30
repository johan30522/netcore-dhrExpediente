using AppExpedienteDHR.Core.ViewModels.User;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> GetUser(string id);
        Task<UserViewModel> GetUserByEmail(string email);
        Task<UserViewModel> GetUserByUserName(string userName);
        Task<List<string>> GetUserRoles(string id);
        Task CreateUser(UserViewModel userViewModel, List<string> SelectedRoles);
        Task<IEnumerable<SelectListItem>> GetListUserRoles();
        Task UpdateUser(UserViewModel userViewModel, List<string> SelectedRoles);
        Task DeleteUser(string id);
        Task<bool> HasUserNameAsync(string userName);
        Task<bool> HasUserEmailAsync(string email);
        Task<IEnumerable<UserViewModel>> SearchUser(string search);



    }
}
