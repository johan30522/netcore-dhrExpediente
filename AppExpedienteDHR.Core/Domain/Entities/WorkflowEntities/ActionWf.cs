
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class ActionWf
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? Order { get; set; }
        public int? NextStateId { get; set; }
        [StringLength(50)]
        public string EvaluationType { get; set; } // Static or Rule
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("NextStateId")]
        public StateWf? NextState { get; set; }
        public ICollection<StateActionWf> StateActions { get; set; } = new List<StateActionWf>();
        public ICollection<ActionRuleWf> ActionRules { get; set; } = new List<ActionRuleWf>();
        public ICollection<ActionGroupWf> ActionGroups { get; set; } = new List<ActionGroupWf>();
    }
}
