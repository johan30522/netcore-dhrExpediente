using AppExpedienteDHR.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class FlowHistoryWf:CommonProps
    {

        public int RequestFlowHeaderId { get; set; } // Relación con la tabla RequestFlowWf
        public int PreviousStateId { get; set; } // Id del estado anterior
        public int NewStateId { get; set; } // Id del nuevo estado
        public int ActionPerformedId { get; set; } // Id de la acción realizada
        [Required]
        [StringLength(450)]
        public string PerformedByUserId { get; set; } // Id del usuario que realizó la acción
        public DateTime ActionDate { get; set; } = DateTime.Now;
        [Required]
        public string Comments { get; set; }


        // Navigation properties
        [ForeignKey("RequestFlowHeaderId")]
        public FlowRequestHeaderWf RequestFlowHeader { get; set; }
        [ForeignKey("PreviousStateId")]
        public StateWf? PreviousState { get; set; }
        [ForeignKey("NewStateId")]
        public StateWf? NewState { get; set; }
        [ForeignKey("ActionPerformedId")]
        public ActionWf? ActionPerformed { get; set; }
        [ForeignKey("PerformedByUserId")]
        public ApplicationUser? PerformedByUser { get; set; } // Reference to Identity User


    }
}
