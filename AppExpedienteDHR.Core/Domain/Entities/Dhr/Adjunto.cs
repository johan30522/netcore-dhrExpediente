using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("Adjuntos", Schema = "dhr")]
    public class Adjunto
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string NombreOriginal { get; set; }  // Nombre que sube el usuario
        [StringLength(200)]
        [Required]
        public string Ruta { get; set; }  // Nombre en disco (GUID + extensión)
        [Required]
        public DateTime FechaSubida { get; set; } = DateTime.Now;
    }
}
