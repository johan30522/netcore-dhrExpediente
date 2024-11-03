using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Admin
{
    public class DerechoViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no debe superar los 50 caracteres")]
        [Display(Name = "Código")]
        [Remote(action: "ExistDerechoCodeValidation", areaName: "Admin", controller: "Tipologia", AdditionalFields = "Id")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no debe superar los 200 caracteres")]
        public string Descripcion { get; set; }

        // Relación con los eventos
        public List<EventoViewModel>? Eventos { get; set; } = new List<EventoViewModel>();
    }
}
