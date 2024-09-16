

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("DenunciaAdjuntos", Schema = "dhr")]
    public class DenunciaAdjunto
    {
        [Key]
        public int Id { get; set; }
        public int DenunciaId { get; set; }
        // Navegaciones
        [ForeignKey("DenunciaId")]
        public Denuncia Denuncia { get; set; }
        public int AdjuntoId { get; set; }
        [ForeignKey("AdjuntoId")]
        public Adjunto Adjunto { get; set; }
    }
}
