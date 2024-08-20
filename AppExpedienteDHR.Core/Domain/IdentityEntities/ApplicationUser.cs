using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(256)]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección")]
        [MaxLength(256)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(256)]
        [Display(Name = "Puesto")]
        public string? Position { get; set; }



        // Relación con los roles
        public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>(); // Cambiado para usar ApplicationUserRole

    }
}
