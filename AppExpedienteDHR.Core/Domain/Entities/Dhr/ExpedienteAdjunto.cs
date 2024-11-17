using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    public class ExpedienteAdjunto
    {
        public int Id { get; set; }
        public int ExpedienteId { get; set; }
        public int AdjuntoId { get; set; }

        [ForeignKey("ExpedienteId")]
        public Expediente Expediente { get; set; }
        [ForeignKey("AdjuntoId")]
        public Adjunto Adjunto { get; set; }
    }
}
