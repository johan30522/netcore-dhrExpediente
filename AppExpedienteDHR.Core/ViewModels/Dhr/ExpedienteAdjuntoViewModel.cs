using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ExpedienteAdjuntoViewModel
    {
        public int Id { get; set; }
        public int ExpedienteId { get; set; }
        public int AdjuntoId { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaSubida { get; set; }

        // Relacionados
        public ExpedienteViewModel Expediente { get; set; }
    }
}
