
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("Denuncia", Schema = "dhr")]
    public class Denuncia
    {
        [Key]
        public int DenunciaId { get; set; }
        public int DenuncianteId { get; set; }
        [StringLength(2000)]
        public string DetalleDenuncia { get; set; }
        [StringLength(1000)]
        public string Petitoria { get; set; }
        public DateTime FechaDenuncia { get; set; } = DateTime.Now;
        public int? PersonaAfectadaId { get; set; }
        public bool AceptaTerminos { get; set; }

        // Navegaciones
        [ForeignKey("DenuncianteId")]
        public Denunciante Denunciante { get; set; }
        [ForeignKey("PersonaAfectadaId")]
        public PersonaAfectada PersonaAfectada { get; set; }
        public ICollection<DenunciaAdjunto> DenunciaAdjuntos { get; set; }

        // Relación con Expediente
        public Expediente Expediente { get; set; }

    }
}
