using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Sexo", Schema = "gen")]
    public class Sexo
    {
        [Key]
        public int SexoId { get; set; }

        [MaxLength(50)]
        public string Descripcion { get; set; }
    }
}
