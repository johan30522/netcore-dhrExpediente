
using System.ComponentModel.DataAnnotations;


namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class ActionWfViewModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public string EvaluationType { get; set; }= "Static";
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Orden")]
        public int? Order { get; set; }
        public int StateId { get; set; }
        public int? NextStateId { get; set; }

        public int? FlowId { get; set; }

        public List<ActionRuleWfViewModel>? Rules { get; set; } = new List<ActionRuleWfViewModel>();
    }
}
