
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class FlowStateWf
    {
        public int Id { get; set; }
        public int FlowId { get; set; }
        public int StateId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("FlowId")]
        public FlowWf? Flow { get; set; }
        [ForeignKey("StateId")]
        public StateWf? State { get; set; }
    }

}

