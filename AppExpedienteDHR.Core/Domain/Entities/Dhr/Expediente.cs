
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    public class Expediente
    {
        [Key]
        public int ExpedienteId { get; set; }
        public int DenunciaId { get; set; }
        public int DenuncianteId { get; set; }
        [StringLength(100)]
        public string EstadoActual { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Navegaciones
        [ForeignKey("DenunciaId")]
        public Denuncia Denuncia { get; set; }
        [ForeignKey("DenuncianteId")]
        public Denunciante Denunciante { get; set; }
    }
}
