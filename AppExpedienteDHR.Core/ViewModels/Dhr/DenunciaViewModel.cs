

using AppExpedienteDHR.Core.Domain.Entities.General;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class DenunciaViewModel
    {
        public int Id { get; set; }
        public int DenuncianteId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Detalle de Denuncia")]
        public string DetalleDenuncia { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Petitoria")]
        public string Petitoria { get; set; }
        public DateTime FechaDenuncia { get; set; } = DateTime.Now;
        public int? PersonaAfectadaId { get; set; }
        public bool AceptaTerminos { get; set; }= false;
        public bool IncluyePersonaAfectada { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public int? LockedRecordId { get; set; }



        // Archivos adjuntos (Subidos por el usuario)
        public IFormFile[]? Files { get; set; } // Aquí se capturan los archivos adjuntos

        // Listas de Catalogos Estaticos
        public IEnumerable<TipoIdentificacion> ListTiposIdentificacion { get; set; }= new List<TipoIdentificacion>();
        public IEnumerable<Sexo> ListSexos { get; set; }= new List<Sexo>();
        public IEnumerable<EstadoCivil> ListEstadosCiviles { get; set; }= new List<EstadoCivil>();
        public IEnumerable<Pais> ListPaises { get; set; }= new List<Pais>();
        public IEnumerable<Escolaridad> ListEscolaridades { get; set; }= new List<Escolaridad>();
        public IEnumerable<Provincia> ListProvincias { get; set; }= new List<Provincia>();



        // Relacionados
        public DenuncianteViewModel Denunciante { get; set; }
        public PersonaAfectadaViewModel? PersonaAfectada { get; set; }
        public IEnumerable<DenunciaAdjuntoViewModel>? DenunciaAdjuntos { get; set; }
    }
}
