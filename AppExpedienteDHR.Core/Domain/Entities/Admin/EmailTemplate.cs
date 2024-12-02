using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.Admin
{
    public class EmailTemplate : CommonProps
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } // Nombre único de la plantilla

        [Required]
        public string SubjectTemplate { get; set; } // Plantilla para el asunto

        [Required]
        public string BodyTemplate { get; set; } // Plantilla HTML enriquecida

        public bool IsActive { get; set; } = true; // Indica si está activa

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
