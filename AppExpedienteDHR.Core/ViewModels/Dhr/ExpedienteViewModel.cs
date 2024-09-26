

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ExpedienteViewModel: ControlFieldsViewModel
    {
        public int Id { get; set; }
        public int? DenunciaId { get; set; }
        public int? DenuncianteId { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Relacionados
        public DenunciaViewModel? Denuncia { get; set; }
        public DenuncianteViewModel? Denunciante { get; set; }
    }
}
