
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AppExpedienteDHR.Core.Domain.IdentityEntities;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class FlowRequestHeaderWf
    {
        [Key]
        public int Id { get; set; } // Primary Key

        public int RequestId { get; set; } // ID de la solicitud, podría ser de cualquier tipo (Denuncia, Expediente, etc.)

        [Required]
        [StringLength(255)]
        public string RequestType { get; set; } // Indicar el tipo de solicitud (Ej: "Denuncia", "Expediente", etc.)

        public int FlowId { get; set; } // Referencia al flujo asociado
        public int CurrentStateId { get; set; } // Estado actual de la solicitud en el flujo

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Fecha de creación de la solicitud en el flujo
        public string CreatedByUserId { get; set; } // Usuario que inició el flujo

        public DateTime? CompletedDate { get; set; } // Fecha de finalización del flujo (opcional)

        public bool IsCompleted { get; set; } = false; // Indicar si el flujo fue completado

        public bool? IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navegación al flujo y estado actual
        [ForeignKey("FlowId")]
        public FlowWf Flow { get; set; }

        [ForeignKey("CurrentStateId")]
        public StateWf CurrentState { get; set; }
        [ForeignKey("CreatedByUserId")]
        public ApplicationUser CreatedByUser { get; set; } // Referencia al usuario que inició el flujo

        // Navegación al historial del flujo
        public ICollection<FlowHistoryWf> FlowHistories { get; set; } = new List<FlowHistoryWf>();

    }
}
