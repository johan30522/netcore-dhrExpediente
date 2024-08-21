
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class RequestFlowHistoryWf
    {
        public int Id { get; set; }
        public int RequestId { get; set; } // Foreign key to the specific request table
        public int FlowHistoryId { get; set; } // Foreign key to FlowHistory

        // Navigation property
        [ForeignKey("FlowHistoryId")]
        public FlowHistoryWf FlowHistory { get; set; }

    }
}
