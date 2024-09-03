using AppExpedienteDHR.Core.ViewModels.Workflow;


namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IActionRuleWfService
    {
        Task<IEnumerable<ActionRuleWfViewModel>> GetActionRules(int actionId);
        Task<ActionRuleWfViewModel> GetActionRule(int id);
        Task CreateActionRule(ActionRuleWfViewModel actionRuleViewModel);
        Task UpdateActionRule(ActionRuleWfViewModel actionRuleViewModel);
        Task DeleteActionRule(int id);
    }
}
