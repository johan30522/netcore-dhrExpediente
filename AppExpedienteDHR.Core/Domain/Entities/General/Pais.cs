using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.General
{
    [Table("Paises", Schema = "gen")]
    public class Pais
    {
        [Key]
        public int CodigoNumerico { get; set; }

        [MaxLength(3)]
        public string CodigoISOAlfa3 { get; set; }

        [MaxLength(2)]
        public string CodigoISOAlfa2 { get; set; }

        [MaxLength(50)]
        public string DenominacionPais { get; set; }
    }
}
