

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    public class DenunciaAdjunto
    {
        [Key]
        public int AdjuntoId { get; set; }
        public int DenunciaId { get; set; }
        [StringLength(150)]
        public string RutaArchivo { get; set; }
        [StringLength(200)]
        public string NombreArchivo { get; set; }
        public DateTime FechaSubida { get; set; } = DateTime.Now;

        // Navegaciones
        [ForeignKey("DenunciaId")]
        public Denuncia Denuncia { get; set; }
    }
}
