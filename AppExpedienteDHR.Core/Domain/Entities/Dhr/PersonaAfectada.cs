

using AppExpedienteDHR.Core.Domain.Entities.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("PersonaAfectada", Schema = "dhr")]
    public class PersonaAfectada
    {
        [Key]
        public int PersonaAfectadaId { get; set; }
        public int TipoIdentificacionId { get; set; }
        [StringLength(50)]
        public string NumeroIdentificacion { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string PrimerApellido { get; set; }
        [StringLength(50)]
        public string? SegundoApellido { get; set; }
        public int SexoId { get; set; }

        // Navegaciones
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public Sexo Sexo { get; set; }

        // Relación con Denuncia
        public ICollection<Denuncia> Denuncias { get; set; }
    }
}
