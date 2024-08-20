using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppExpedienteDHR.Utils.Constants;
using Microsoft.Extensions.Configuration;
using AppExpedienteDHR.Core.ViewModels.User;

namespace AppExpedienteDHR.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;


        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IContainerWork containerWork, IMapper mapper, IConfiguration configuration, IRoleService roleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _containerWork = containerWork;
            _mapper = mapper;
            _configuration = configuration;
            _roleService = roleService;
        }


        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            try
            {
                IEnumerable<ApplicationUser> users = await _containerWork.User.GetAll(includeProperties: "UserRoles.Role");

                IEnumerable<UserViewModel> userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

                return userViewModels;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios", ex);
            }
        }

        public Task<IEnumerable<SelectListItem>> GetListUserRoles()
        {
            return Task.FromResult(new List<SelectListItem>
            {
                new SelectListItem { Text = Roles.Admin, Value = Roles.Admin },
                new SelectListItem { Text = Roles.User, Value = Roles.User }
            }.AsEnumerable());
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            var user = await _containerWork.User.Get(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task CreateUser(UserViewModel userViewModel, List<string> SelectedRoles)
        {


            var user = new ApplicationUser
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
                FullName = userViewModel.FullName,
                Address = userViewModel.Address,
                PhoneNumber = userViewModel.PhoneNumber,
                Position = userViewModel.Position,
                EmailConfirmed = true // Otras propiedades que desees configurar
            };

            string defaultPassword = _configuration["Identity:DefaultPassword"];

            // Crea el usuario con la contraseña predeterminada
            var result = await _userManager.CreateAsync(user, defaultPassword);


            if (result.Succeeded)
            {
                //  agregar roles u otras configuraciones adicionales aquí
                // verifica si los roles seleccionados existen, sino los crea
                foreach (var role in SelectedRoles)
                {
                    await _roleService.CreateRoleAsync(role);
                }
                await _userManager.AddToRolesAsync(user, SelectedRoles);
            }
            else
            {
                // Maneja errores en caso de que la creación no haya sido exitosa
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }



        }

        public async Task<bool> HasUserNameAsync(string userName)
        {

            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            //remove empty spaces
            userName = userName.Trim();
            string emailUpper = userName.ToUpper();
            ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.UserName.ToUpper() == emailUpper);
            return user != null;
        }

        public async Task<bool> HasUserEmailAsync(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            //remove empty spaces
            email = email.Trim();
            string emailUpper = email.ToUpper();
            ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.Email.ToUpper() == emailUpper);
            return user != null;
        }

        public async Task UpdateUser(UserViewModel userViewModel, List<string> SelectedRoles)
        {

            // get the user to update
            ApplicationUser userToUpdate = await _containerWork.User.GetFirstOrDefault(u => u.Id == userViewModel.Id);
            if (userToUpdate == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            // update the user
            userToUpdate.UserName = userViewModel.UserName;
            userToUpdate.Email = userViewModel.Email;
            userToUpdate.FullName = userViewModel.FullName;
            userToUpdate.Address = userViewModel.Address;
            userToUpdate.PhoneNumber = userViewModel.PhoneNumber;
            userToUpdate.Position = userViewModel.Position;

            // update the user
            var result = await _userManager.UpdateAsync(userToUpdate);

            if (result.Succeeded)
            {
                // get the roles of the user
                IList<string> userRoles = await _userManager.GetRolesAsync(userToUpdate);
                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(userToUpdate, role);
                }

                // verifica si los roles seleccionados existen, sino los crea
                foreach (var role in SelectedRoles)
                {
                    await _roleService.CreateRoleAsync(role);
                }
                await _userManager.AddToRolesAsync(userToUpdate, SelectedRoles);
            }
            else
            {
                // Maneja errores en caso de que la actualización no haya sido exitosa
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }


        }

        public async Task DeleteUser(string id)
        {

            ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            var result = await _userManager.DeleteAsync(user);
        }

        public async Task<List<string>> GetUserRoles(string id)
        {
            ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            var user = await _containerWork.User.GetFirstOrDefault(u => u.Email == email);
            return _mapper.Map<UserViewModel>(user);

        }


        public async Task<UserViewModel> GetUserByUserName(string userName)
        {
            var user = await _containerWork.User.GetFirstOrDefault(u => u.UserName == userName);
            return _mapper.Map<UserViewModel>(user);

        }
    }
}
