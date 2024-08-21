
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class ActionRuleWf
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? Order { get; set; }
        [StringLength(50)]
        public string FieldEvaluated { get; set; }
        [StringLength(20)]
        public string Operator { get; set; }
        [StringLength(100)]
        public string ComparisonValue { get; set; }
        public int ResultStateId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("ActionId")]
        public ActionWf? Action { get; set; }
        [ForeignKey("ResultStateId")]
        public StateWf? ResultState { get; set; }
    }
}
