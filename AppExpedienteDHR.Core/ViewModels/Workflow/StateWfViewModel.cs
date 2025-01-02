

using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class StateWfViewModel
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

        public bool IsInitialState { get; set; }
        public bool IsFinalState { get; set; }
        public bool IsNotificationActive { get; set; } = false;

        public List<ActionWfViewModel>? Actions { get; set; } = new List<ActionWfViewModel>();

        public StateNotificationWfViewModel? StateNotification { get; set; } = new StateNotificationWfViewModel();

    }
}
