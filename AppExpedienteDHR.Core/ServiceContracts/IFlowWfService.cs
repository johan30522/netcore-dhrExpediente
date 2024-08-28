using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IFlowWfService
    {
        Task<IEnumerable<FlowWfViewModel>> GetFlows();
        Task<FlowWfViewModel> GetFlow(int id);
        Task<FlowWfViewModel> CreateFlow(FlowWfViewModel flowViewModel);
        Task<FlowWfViewModel> UpdateFlow(FlowWfViewModel flowViewModel);
        Task DeleteFlow(int id);
 
    }
}
