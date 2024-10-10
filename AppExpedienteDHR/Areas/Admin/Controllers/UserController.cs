
using AppExpedienteDHR.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppExpedienteDHR.Core.ViewModels.User;
using AppExpedienteDHR.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using AppExpedienteDHR.Core.Models;

namespace AppExpedienteDHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "User"), IsActive = false }
                };
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create ()
        {
            UserViewModel userViewModel = new UserViewModel();
            IEnumerable<SelectListItem> roles = await _userService.GetListUserRoles();
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "User"), IsActive = false },
                    new Breadcrumb { Title = "Crear Usuario", Url = Url.Action("Create", "User"), IsActive = true }
                };

            UserFormViewModel userFormViewModel = new UserFormViewModel
            {
                User = userViewModel,
                RolesItems = roles
            };

            return View(userFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(userViewModel.User, userViewModel.SelectedRoles);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserViewModel userViewModel = await _userService.GetUser(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            ViewData["Breadcrumbs"] = new List<Breadcrumb>
                {
                    new Breadcrumb { Title = "Configuración", Url = Url.Action("Index", "User"), IsActive = false },
                    new Breadcrumb { Title = "Editar Usuario", Url = Url.Action("Create", "User"), IsActive = true }
                };
            List<string> userRoles = await _userService.GetUserRoles(id);
            IEnumerable<SelectListItem> roles = await _userService.GetListUserRoles();

            UserFormViewModel userFormViewModel = new UserFormViewModel
            {
                User = userViewModel,
                RolesItems = roles,
                SelectedRoles = userRoles
            };

            return View(userFormViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserFormViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(userViewModel.User, userViewModel.SelectedRoles);
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        #region API CALLS

        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsers();
            return Json(new { data = users });
        }

        [HttpGet]
        public async Task<IActionResult> ExistUserValidation([FromQuery(Name = "User.UserName")] string username, [FromQuery(Name = "User.Id")] string id)
        {
            UserViewModel user = await _userService.GetUserByUserName(username);
            if (user != null && user.Id != id)
            {
                return Json("El usuario ya existe");
            }
     
            return Json(true);
        }
        [HttpGet]
        public async Task<IActionResult> ExistEmailValidation([FromQuery(Name = "User.Email")] string email, [FromQuery(Name = "User.Id")] string id)
        {
            UserViewModel user = await _userService.GetUserByEmail(email);
            if(user != null && user.Id != id)
            {
                return Json("El email ya existe");
            }
            return Json(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);
            return Json(new { success = true, message = "Eliminado correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsers(string search)
        {
            var users = await _userService.SearchUser(search);
            return Json(new { users });
        }

        #endregion

  
    }
}
