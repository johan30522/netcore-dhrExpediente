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
    public class FlowHistoryWf
    {
        public int Id { get; set; }
        public int FlowId { get; set; }
        public int PreviousStateId { get; set; }
        public int NewStateId { get; set; }
        public int ActionPerformedId { get; set; }
        [Required]
        public string PerformedByUserId { get; set; } // Assuming Identity uses string for UserId
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
        [Required]
        [StringLength(500)]
        public string Comments { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("FlowId")]
        public FlowWf? Flow { get; set; }
        [ForeignKey("PreviousStateId")]
        public StateWf? PreviousState { get; set; }
        [ForeignKey("NewStateId")]
        public StateWf? NewState { get; set; }
        [ForeignKey("ActionPerformedId")]
        public ActionWf? ActionPerformed { get; set; }
        [ForeignKey("PerformedByUserId")]
        public ApplicationUser? PerformedByUser { get; set; } // Reference to Identity User

        public ICollection<RequestFlowHistoryWf> RequestFlowHistories { get; set; } = new List<RequestFlowHistoryWf>();

    }
}
