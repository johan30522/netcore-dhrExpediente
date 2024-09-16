using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ExpedienteViewModel
    {
        public int Id { get; set; }
        public int DenunciaId { get; set; }
        public int DenuncianteId { get; set; }
        public string EstadoActual { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relacionados
        public DenunciaViewModel Denuncia { get; set; }
        public DenuncianteViewModel Denunciante { get; set; }
    }
}
