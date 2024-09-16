using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Provincias", Schema = "gen")]
    public class Provincia
    {
        [Key]
        public int Codigo { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [Column("Area_Km2")]
        public decimal? AreaKm2 { get; set; }

        public ICollection<Canton> Cantones { get; set; }
    }
}
