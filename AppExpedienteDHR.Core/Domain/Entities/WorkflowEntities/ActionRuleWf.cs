
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class ActionRuleWf:CommonProps
    {
        public int ActionId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public int? Order { get; set; }
        [Required]
        public int ResultStateId { get; set; }

        public string? RuleJson { get; set; } //  Para definir reglas en json


        // Navigation properties
        [ForeignKey("ActionId")]
        public ActionWf? Action { get; set; }
        [ForeignKey("ResultStateId")]
        public StateWf? ResultState { get; set; }
    }
}
