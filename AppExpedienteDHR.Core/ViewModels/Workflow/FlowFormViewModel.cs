

namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class FlowFormViewModel
    {
        public FlowWfViewModel Flow { get; set; }

        public List<GroupWfViewModel>? Groups { get; set; } = new List<GroupWfViewModel>();
        public List<StateWfViewModel>? States { get; set; } = new List<StateWfViewModel>();

        // Propiedades para crear nuevos grupos y estados
        public GroupWfViewModel? Group { get; set; } = new GroupWfViewModel();
        public StateWfViewModel? State { get; set; } = new StateWfViewModel();

    }
}
