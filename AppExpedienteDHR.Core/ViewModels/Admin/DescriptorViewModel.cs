using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Admin
{
    public class DescriptorViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no debe superar los 50 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre no debe superar los 200 caracteres")]
        public string Nombre { get; set; }

        // Relación con el evento
        public int? EventoId { get; set; }

    }
}
