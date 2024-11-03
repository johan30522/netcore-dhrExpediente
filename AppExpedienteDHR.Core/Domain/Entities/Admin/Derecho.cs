using AppExpedienteDHR.Core.Domain.EntityContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.Admin
{
    [Table("Derechos", Schema = "adm")]
    public class Derecho : CommonProps, ISoftDeletable
    {

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        // Relación con los eventos
        public ICollection<Evento> Eventos { get; set; }
    }
}
