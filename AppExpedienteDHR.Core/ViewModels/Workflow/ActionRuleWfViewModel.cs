

using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class ActionRuleWfViewModel
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Orden")]
        public int Order { get; set; }

        public string? RuleJson { get; set; } // JSON string to store complex rules

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Estado de resultado")]
        public int ResultStateId { get; set; }

    }
}
