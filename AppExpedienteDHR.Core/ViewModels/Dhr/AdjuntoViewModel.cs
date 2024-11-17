
using System.ComponentModel.DataAnnotations;


namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class AdjuntoViewModel
    {
            public int Id { get; set; }
            [StringLength(200)]
            [Required]
            public string NombreOriginal { get; set; }  // Nombre que sube el usuario
            [StringLength(200)]
            [Required]
            public string Ruta { get; set; }  // Nombre en disco (GUID + extensión)
            [Required]
            public DateTime FechaSubida { get; set; } = DateTime.Now;
    }
}
