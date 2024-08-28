
using System.ComponentModel.DataAnnotations;


namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class GroupWfViewModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Orden")]
        public int? Order { get; set; }

        public int FlowWfId { get; set; }
    }
}
