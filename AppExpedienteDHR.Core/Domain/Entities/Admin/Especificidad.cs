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
    [Table("Especifidades", Schema = "adm")]
    public class Especificidad : CommonProps, ISoftDeletable
    {

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [StringLength(200)]
        public string Normativa { get; set; }

        // Relación con el evento
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
