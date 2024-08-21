
using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class StateWf
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? Order { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public ICollection<FlowStateWf> FlowStates { get; set; } = new List<FlowStateWf>();
        public ICollection<StateActionWf> StateActions { get; set; } = new List<StateActionWf>();
        public ICollection<ActionRuleWf> ActionRules { get; set; } = new List<ActionRuleWf>();
    }
}
