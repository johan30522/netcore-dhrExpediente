using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Admin
{
    public class EventoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no debe superar los 50 caracteres")]
        [Remote(action: "ExistEventoCodeValidation", areaName: "Admin", controller: "Tipologia", AdditionalFields = "Id")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no debe superar los 200 caracteres")]
        public string Descripcion { get; set; }

        [StringLength(200, ErrorMessage = "La normativa no debe superar los 200 caracteres")]
        public string? Normativa { get; set; }

        [StringLength(200, ErrorMessage = "El ODS no debe superar los 200 caracteres")]
        public string? ODS { get; set; }

        // Relación con los descriptores y especificidades
        public List<DescriptorViewModel>? Descriptores { get; set; } = new List<DescriptorViewModel>();
        public List<EspecificidadViewModel>? Especificidades { get; set; } = new List<EspecificidadViewModel>();

        // Relación con el derecho
        public int? DerechoId { get; set; }
    }
}
