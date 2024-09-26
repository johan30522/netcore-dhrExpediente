
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
        [StringLength(100)]
        public string Detalle { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Navegaciones
        [ForeignKey("DenunciaId")]
        public Denuncia? Denuncia { get; set; }
        [ForeignKey("DenuncianteId")]
        public Denunciante? Denunciante { get; set; }
    }
}
