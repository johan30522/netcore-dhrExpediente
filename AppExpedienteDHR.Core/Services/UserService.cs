using AppExpedienteDHR.Core.Domain.IdentityEntities;
using AppExpedienteDHR.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppExpedienteDHR.Utils.Constants;
using Microsoft.Extensions.Configuration;
using AppExpedienteDHR.Core.ViewModels.User;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using Microsoft.AspNetCore.Http;

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
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IContainerWork containerWork, IMapper mapper, IConfiguration configuration, IRoleService roleService, ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _containerWork = containerWork;
            _mapper = mapper;
            _configuration = configuration;
            _roleService = roleService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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
                _logger.Error(ex, "Error al obtener los usuarios");
                throw;
            }
        }

        public Task<IEnumerable<SelectListItem>> GetListUserRoles()
        {
            try
            {
                return Task.FromResult(new List<SelectListItem>
                {
                    new SelectListItem { Text = Roles.Admin, Value = Roles.Admin },
                    new SelectListItem { Text = Roles.User, Value = Roles.User }
                }.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los roles de usuario");
                throw;
            }
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            try
            {
                var user = await _containerWork.User.Get(id);
                return _mapper.Map<UserViewModel>(user);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario");
                throw;
            }
        }

        public async Task CreateUser(UserViewModel userViewModel, List<string> SelectedRoles)
        {

            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear el usuario");
                throw;
            }
        }

        public async Task<bool> HasUserNameAsync(string userName)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al verificar si el usuario existe");
                throw;
            }

        }

        public async Task<bool> HasUserEmailAsync(string email)
        {
            try
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
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al verificar si el correo existe");
                throw;
            }

        }

        public async Task UpdateUser(UserViewModel userViewModel, List<string> SelectedRoles)
        {
            try
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
                    _logger.Error("Error al actualizar el usuario: {0}", string.Join("; ", result.Errors.Select(e => e.Description)));
                    throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el usuario");
                throw;
            }


        }

        public async Task DeleteUser(string id)
        {
            try
            {

                ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                var result = await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el usuario");
                throw new Exception("Error al eliminar el usuario", ex);
            }

        }

        public async Task<List<string>> GetUserRoles(string id)
        {
            try
            {
                ApplicationUser user = await _containerWork.User.GetFirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                return (List<string>)await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los roles del usuario");
                throw;
            }
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            try
            {
                var user = await _containerWork.User.GetFirstOrDefault(u => u.Email == email);
                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario por correo");
                throw;
            }

        }


        public async Task<UserViewModel> GetUserByUserName(string userName)
        {
            try
            {
                var user = await _containerWork.User.GetFirstOrDefault(u => u.UserName == userName);
                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario por nombre de usuario");
                throw;
            }

        }

        public async Task<IEnumerable<UserViewModel>> SearchUser(string search)
        {
            try
            {
                // buscar usuarios por nombre, correo, etc
                IEnumerable<ApplicationUser> users = await _containerWork.User.GetAll(u => u.UserName.Contains(search) || u.Email.Contains(search) || u.FullName.Contains(search));
                IEnumerable<UserViewModel> userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
                return userViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario por nombre de usuario");
                throw;
            }
        }

        public async Task<UserViewModel> GetCurrentUser()
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario actual");
                throw;
            }
        }
    }
}
