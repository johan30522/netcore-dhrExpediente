using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IActionWfService
    {
        Task<IEnumerable<ActionWfViewModel>> GetActions();
        Task<ActionWfViewModel> GetAction(int id);
        Task CreateAction(ActionWfViewModel actionViewModel);
        Task UpdateAction(ActionWfViewModel actionViewModel);
        Task DeleteAction(int id);
    }
}
