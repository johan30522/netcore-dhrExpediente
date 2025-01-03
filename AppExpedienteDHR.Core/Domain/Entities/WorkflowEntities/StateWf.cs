﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class StateWf:CommonProps
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public int? Order { get; set; }

        public bool IsInitialState { get; set; } // indica si es el estado inicial
        public bool IsFinalState { get; set; } // Indica si es el estado final

        public bool IsNotificationActive { get; set; } = false; // Indica si se envía notificación




        // Foreign Key y navegación hacia FlowWf
        public int FlowId { get; set; }
        [ForeignKey("FlowId")]
        public FlowWf Flow { get; set; }

        // Relación uno a muchos con ActionWf
        public ICollection<ActionWf> Actions { get; set; } = new List<ActionWf>();
        public StateNotificationWf? StateNotification { get; set; }
    }
}
