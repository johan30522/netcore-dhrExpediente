
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class FlowGroupWf
    {
        public int Id { get; set; }
        public int FlowId { get; set; }
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("FlowId")]
        public FlowWf? Flow { get; set; }
        [ForeignKey("GroupId")]
        public GroupWf? Group { get; set; }
    }
}
