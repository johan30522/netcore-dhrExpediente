

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ExpedienteItemListViewModel
    {
        public int ExpedienteId { get; set; }
        public string ExpedienteDetalle { get; set; }

        public string DenuncianteFullName { get; set; }

        public DateTime ExpedienteFechaCreacion { get; set; }
        public string EstadoActual { get; set; } // Nombre del estado actual del flujo
        public string TipoSolicitud { get; set; } // Por si se usa para otros tipos
        public DateTime FechaCreacionFlujo { get; set; }
        public bool FlujoCompletado { get; set; }
    }
}
