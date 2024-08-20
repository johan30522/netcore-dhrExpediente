using AppExpedienteDHR.Core.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.User
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre de Usuario")]
        [Remote(action: "ExistUserValidation", areaName: "Admin", controller: "User")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo válida")]
        [Remote(action: "ExistEmailValidation", areaName: "Admin", controller: "User")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre Completo")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Teléfono")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Posición")]
        public string? Position { get; set; }

        //Roles del usuario
        public IEnumerable<RoleViewModel>? Roles { get; set; }
    }
}
