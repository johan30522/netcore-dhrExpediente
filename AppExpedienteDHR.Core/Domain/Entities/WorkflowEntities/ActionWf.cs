
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class ActionWf
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public int? Order { get; set; }
        public int StateId { get; set; }
        public int? NextStateId { get; set; }
        [Required]
        [StringLength(50)]
        public string EvaluationType { get; set; } // Static or Rule
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("NextStateId")]
        public StateWf? NextState { get; set; }
        // Relación muchos a uno con StateWf
        [ForeignKey("StateId")]
        public StateWf State { get; set; }

        // Relación uno a muchos con ActionRuleWf
        public ICollection<ActionRuleWf> ActionRules { get; set; } = new List<ActionRuleWf>();

        // Relación uno a muchos con ActionGroupWf
        public ICollection<ActionGroupWf> ActionGroups { get; set; } = new List<ActionGroupWf>();
    }
}
