

using AppExpedienteDHR.Core.Domain.Entities.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("PersonasAfectada", Schema = "dhr")]
    public class PersonaAfectada
    {
        [Key]
        public int Id { get; set; }
        public int TipoIdentificacionId { get; set; }
        [StringLength(50)]
        [Required]
        public string NumeroIdentificacion { get; set; }
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        [StringLength(50)]
        [Required]
        public string PrimerApellido { get; set; }
        [StringLength(50)]
        [Required]
        public string? SegundoApellido { get; set; }
        public int SexoId { get; set; }

        // Navegaciones
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public Sexo Sexo { get; set; }

        // Relación con Denuncia
        public ICollection<Denuncia> Denuncias { get; set; }
    }
}
