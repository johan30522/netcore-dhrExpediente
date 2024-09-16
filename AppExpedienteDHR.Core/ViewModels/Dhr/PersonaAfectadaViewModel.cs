

using AppExpedienteDHR.Core.Domain.Entities.General;
using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class PersonaAfectadaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo de Identificación")]
        public int TipoIdentificacionId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Número de Identificación")]
        public string NumeroIdentificacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Segundo Apellido")]
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public int SexoId { get; set; }

        // Relacionados
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public Sexo Sexo { get; set; }
    }
}
