namespace AppExpedienteDHR.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? Message { get; set; } // Mensaje breve del error
        public string? ErrorId { get; set; } // Identificador único del error
        public string? TechnicalDetails { get; set; } // Detalles técnicos del error
        public string? ExceptionCode { get; set; } // Código de la excepción
    }
}
