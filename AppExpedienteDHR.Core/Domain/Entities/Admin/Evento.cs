using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.Admin
{
    [Table("Eventos", Schema = "adm")]
    public class Evento: CommonProps
    {

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [StringLength(200)]
        public string Normativa { get; set; }

        [StringLength(200)]
        public string ODS { get; set; }

        // Relación con los descriptores
        public ICollection<Descriptor> Descriptores { get; set; }

        // Relación con las especificidades
        public ICollection<Especificidad> Especificidades { get; set; }

        // Relación con el derecho
        public int DerechoId { get; set; }
        public Derecho Derecho { get; set; }
    }
}
