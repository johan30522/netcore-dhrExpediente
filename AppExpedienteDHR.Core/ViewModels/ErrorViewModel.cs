namespace AppExpedienteDHR.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string? Message { get; set; } // Mensaje breve del error
        public string? ErrorId { get; set; } // Identificador �nico del error
        public string? TechnicalDetails { get; set; } // Detalles t�cnicos del error
        public string? ExceptionCode { get; set; } // C�digo de la excepci�n
    }
}
