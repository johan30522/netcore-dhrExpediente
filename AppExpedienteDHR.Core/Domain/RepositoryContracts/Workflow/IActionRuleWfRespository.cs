using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow
{
    public interface IActionRuleWfRespository: IRepository<ActionRuleWf>
    {
        Task Update(ActionRuleWf actionRule);
    }
}
