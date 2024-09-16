using AppExpedienteDHR.Core.Domain.Entities.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class DenuncianteViewModel
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
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado Civil")]
        public int EstadoCivilId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "País de Origen")]
        public int PaisOrigenCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Escolaridad")]
        public int EscolaridadId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Teléfono")]
        public string TelefonoCelular { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = "Debe ingresar un una dirección válida.")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Es Menor de Edad")]
        public bool EsMenorEdad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tiene Requerimiento Especial")]
        public bool TieneRequerimientoEspecial { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Provincia")]
        public int ProvinciaCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantón")]
        public int CantonCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Distrito")]
        public int DistritoCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección Exacta")]
        public string? DireccionExacta { get; set; }

        // Relacionados
        public TipoIdentificacion? TipoIdentificacion { get; set; }
        public Sexo? Sexo { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public Pais? PaisOrigen { get; set; }
        public Escolaridad? Escolaridad { get; set; }
        public Provincia? Provincia { get; set; }
        public Canton? Canton { get; set; }
        public Distrito? Distrito { get; set; }
    }
}
