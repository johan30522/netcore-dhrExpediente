using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Distritos", Schema = "gen")]
    public class Distrito
    {
        [Key]
        public int CodigoDistrito { get; set; }

        public int? CodigoCanton { get; set; }

        [MaxLength(50)]
        public string NombreDistrito { get; set; }

        public decimal? AreaDistritoKm2 { get; set; }

        [ForeignKey("CodigoCanton")]
        public Canton Canton { get; set; }
    }
}
