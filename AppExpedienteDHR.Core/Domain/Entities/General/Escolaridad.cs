using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Escolaridad", Schema = "gen")]
    public class Escolaridad
    {
        [Key]
        public int EscolaridadId { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }
    }
}
