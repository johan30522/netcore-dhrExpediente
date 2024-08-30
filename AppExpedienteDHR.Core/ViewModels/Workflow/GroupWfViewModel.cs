
using AppExpedienteDHR.Core.ViewModels.User;
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

        // Lista de usuarios seleccionados
        public List<string>? SelectedUserIds { get; set; } = new List<string>();


        //  usuarios asociados
        public List<UserViewModel>? Users { get; set; } = new List<UserViewModel>();
    }
}
