
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("Expedientes", Schema = "dhr")]
    public class Expediente:CommonProps
    {
        //public int Id { get; set; }
        public int? DenunciaId { get; set; }
        public int? DenuncianteId { get; set; }
        public int? PersonaAfectadaId { get; set; }
        [StringLength(2000)]
        public string Detalle { get; set; }

        [StringLength(1000)]
        public string? Petitoria { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public bool IncluyePersonaAfectada { get; set; } = false;

        // Navegaciones
        [ForeignKey("DenunciaId")]
        public Denuncia? Denuncia { get; set; }
        [ForeignKey("DenuncianteId")]
        public Denunciante? Denunciante { get; set; }
        [ForeignKey("PersonaAfectadaId")]
        public PersonaAfectada PersonaAfectada { get; set; }
    }
}
