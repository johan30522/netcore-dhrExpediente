using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IStateWfService
    {
        Task<IEnumerable<StateWfViewModel>> GetStates(int flowId);
        Task<StateWfViewModel> GetState(int id);
        Task CreateState(StateWfViewModel stateViewModel, int flowId);
        Task UpdateState(StateWfViewModel stateViewModel);
        Task DeleteState(int id);
    }
}
