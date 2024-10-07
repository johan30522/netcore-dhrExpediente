

using AppExpedienteDHR.Core.Domain.Entities.General;
using Microsoft.AspNetCore.Http;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ExpedienteViewModel: ControlFieldsViewModel
    {
        public int Id { get; set; }
        public int? DenunciaId { get; set; }
        public int? DenuncianteId { get; set; }
        public string Detalle { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Petitoria { get; set; }
        public DateTime? FechaDenuncia { get; set; } = DateTime.Now;
        public int? PersonaAfectadaId { get; set; }
        public bool? IncluyePersonaAfectada { get; set; } = false;


        // Archivos adjuntos (Subidos por el usuario)
        public IFormFile[]? Files { get; set; } // Aquí se capturan los archivos adjuntos



        // Relacionados
        public DenunciaViewModel? Denuncia { get; set; }
        public DenuncianteViewModel? Denunciante { get; set; }
        public PersonaAfectadaViewModel? PersonaAfectada { get; set; }
        public IEnumerable<DenunciaAdjuntoViewModel>? DenunciaAdjuntos { get; set; }


        // Listas de Catalogos Estaticos
        public IEnumerable<TipoIdentificacion> ListTiposIdentificacion { get; set; } = new List<TipoIdentificacion>();
        public IEnumerable<Sexo> ListSexos { get; set; } = new List<Sexo>();
        public IEnumerable<EstadoCivil> ListEstadosCiviles { get; set; } = new List<EstadoCivil>();
        public IEnumerable<Pais> ListPaises { get; set; } = new List<Pais>();
        public IEnumerable<Escolaridad> ListEscolaridades { get; set; } = new List<Escolaridad>();
        public IEnumerable<Provincia> ListProvincias { get; set; } = new List<Provincia>();
    }
}
