using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Admin
{
    public class EspecificidadViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no debe superar los 50 caracteres")]
        [Remote(action: "ExistEspecificidadCodeValidation", areaName: "Admin", controller: "Tipologia", AdditionalFields = "Id")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no debe superar los 200 caracteres")]
        public string Descripcion { get; set; }

        [StringLength(200, ErrorMessage = "La normativa no debe superar los 200 caracteres")]
        public string? Normativa { get; set; }

        // Relación con el evento
        public int? EventoId { get; set; }
    }
}
