using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Cantones", Schema = "gen")]
    public class Canton
    {
        [Key]
        public int CodigoCanton { get; set; }

        public int? CodigoProvincia { get; set; }

        [MaxLength(50)]
        public string NombreCanton { get; set; }

        public decimal? AreaCantonKm2 { get; set; }

        [ForeignKey("CodigoProvincia")]
        public Provincia Provincia { get; set; }

        public ICollection<Distrito> Distritos { get; set; }
    }
}
