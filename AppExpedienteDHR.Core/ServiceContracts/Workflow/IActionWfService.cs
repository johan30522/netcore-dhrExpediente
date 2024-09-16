using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts.Workflow
{
    public interface IActionWfService
    {
        Task<IEnumerable<ActionWfViewModel>> GetActions(int stateId);
        Task<ActionWfViewModel> GetAction(int id);
        Task CreateAction(ActionWfViewModel actionViewModel);
        Task UpdateAction(ActionWfViewModel actionViewModel);
        Task DeleteAction(int id);
    }
}
