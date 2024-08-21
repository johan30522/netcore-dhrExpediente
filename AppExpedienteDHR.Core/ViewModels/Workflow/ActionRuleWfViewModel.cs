

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
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Campo a evaluar")]
        public string FieldEvaluated { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Operador")]
        public string Operator { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Valor de comparación")]
        public string ComparisonValue { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Estado de resultado")]
        public int ResultStateId { get; set; }

    }
}
